<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TimesheetProject.aspx.cs" Inherits="SMEPayroll.TimeSheet.TimesheetProject" %>
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
            .hiddencol
            {
                display:none;
            }
            .viscol
            {
                display:block;
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
    
    html, body, form   
    {   
       /*height: 100%;   */
       height: 100%;
       margin: 0px;   
       padding: 0px;  
       overflow: auto;
    }   


</style>
      


   

</head>
<body style="margin-left:auto;">
    <form id="form1" runat="server"  method="post">
     <telerik:RadScriptManager ID="RadScriptManager1" Runat="server" >
    </telerik:RadScriptManager> 
    
        
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1"   AnimationDuration="1500"  runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
            <asp:Image ID="Image1"  runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    
    
    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
        
            <%-- <telerik:AjaxSetting AjaxControlID="RadGrid2">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"/>
                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"/>
                            
                        </UpdatedControls>
            </telerik:AjaxSetting>      --%>  
        
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     
                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            
<%--              <telerik:AjaxSetting AjaxControlID="btnSubApprove">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     
                     
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            
            
            <telerik:AjaxSetting AjaxControlID="btnApprove">
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
            
            <telerik:AjaxSetting AjaxControlID="btnUnlock">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
           <%-- <telerik:AjaxSetting AjaxControlID="btnCopy">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            --%>
            
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

         <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
    
   </telerik:RadCodeBlock>
   
        
        <script type="text/javascript" language="javascript">  
        
                    var newURL;
                    var cnt;
                    function GetNewUrl()
                    {
                        var url = window.location.href;
                        var mySplitResult = url.split("/");        
                         for(i = 0; i < mySplitResult.length-2; i++)
                         {
                               if(i==0)
                                {
                                    newURL=mySplitResult[i]+"//";
                                }                
                                else if(mySplitResult[i]!="")
                                {
                                    newURL=newURL+mySplitResult[i]+"/";
                                }
                         }         
                    }

                    function copy1()
                    {
                    
                        return false;
                    }

                    //Copy IN and OUT time 
                    function  Copy()
                    {
                            var grid = $find("<%=RadGrid2.ClientID %>");	                    
                            var masterTableView = grid.get_masterTableView();
                           var selectedRows = masterTableView.get_selectedItems();  
                           var OutTime = $find("<%=DeftxtOutTime.ClientID %>");  
                           var InTime = $find("<%=DeftxtInTime.ClientID %>");  
                           
                           //alert(document.getElementById("DeftxtOutTime").value);
                            
                            for (var j = 0; j < selectedRows.length; j++)
                            {  
                                    //alert(1);
                                    var rowInner= selectedRows[j];                                   
                                    var inTimeCell      = masterTableView.getCellByColumnUniqueName(rowInner, "InShortTime");
                                    var outTimeCell     = masterTableView.getCellByColumnUniqueName(rowInner, "OutShortTime");
                                    
                                    //alert(inTimeCell.getElementsByTagName("input")[0].visible);
                                    
                                    if(inTimeCell.getElementsByTagName("input")[0].disabled)
                                    {
                                        //rowInner.background-color="#FFFFFF !important";
                                    }
                                    else
                                    {
                                     
                                        inTimeCell.getElementsByTagName("input")[0].value=document.getElementById("DeftxtInTime").value;
                                        outTimeCell.getElementsByTagName("input")[0].value=document.getElementById("DeftxtOutTime").value;                        
                                    }
                            }
                            Test(1);
                            return false;
                    }
                    
                    function GridCreated()
                    {
                       Test(1);
                    }
                    
                    function RowClick(sender, eventArgs)
                    { 
                    
                      

                        //                                //alert("Click on row instance: " + eventArgs.get_itemIndexHierarchical());
                        //                                //eventArgs.set_cancel(true);
                        //                        
                       
                        //                                var selectedRows = masterTableView.get_selectedItems();                        
                        //                                //Check Roster Type
                        //                                var rosterType;
                        //                                var msg='';
                        //                                var rowno='';
                        //                              
                        //                               //alert('hi');
                        //                               for (var i = 0; i < selectedRows.length; i++) 
                        //                               { 
                        //                                     var row                  =   selectedRows[i];                                
                        //                                     var cell                 =   masterTableView.getCellByColumnUniqueName(row, "GridClientSelectColumn"); 
                        //                                     alert(cell.innerHTML);
                        //                               }
                        
                        //OnRowClick="RowClick" OnRowSelected="RowDblClick"
                        //eventArgs.set_cancel(true);
                        var grid = $find("<%=RadGrid2.ClientID %>");	                    
                        var masterTableView = grid.get_masterTableView();
                                                
                         if (masterTableView != null) 
                         {
                             var gridItems = masterTableView.get_dataItems();
                             var i;
                             for (i = 0; i < gridItems.length; ++i) 
                             {
                                 var gridItem = gridItems[i];
                                 var cell = gridItem.get_cell("GridClientSelectColumn");
                                 var controlsArray = cell.getElementsByTagName('input');
                                 if (controlsArray.length > 0) 
                                 {
                                     var rdo = controlsArray[0];
                                     rdo.checked='true';
                                 }
                             }
                         }

                    }
                        
                    
                  var flag='true';  
                  
                    function CallFunction(val)
                    {
                        Test(val);
                        //alert(1);
                    }
                
                    
                  function Test(txtIn)
	              {

                        // return;
                        // alert(1);
	                    // cnt=0;

	                    GetNewUrl();
	                    //var ctrl = document.getElementById('RadGrid2');
	                    //var grid = $find("<%=RadGrid2.ClientID %>");
	                    //alert(grid);
	                    //var res = SMEPayroll.TimeSheet.GManualTimesheetDataEntry.Validate1();  	                                   
	                    var grid = $find("<%=RadGrid2.ClientID %>");	                    
                        var masterTableView = grid.get_masterTableView();
                        var selectedRows = masterTableView.get_selectedItems();                        
                        //Check Roster Type
                        var rosterType;
                        var msg='';
                        var rowno='';
                         
//                         var objTXT = $find(txtIn);
//                         
//                         if(objTXT!=null)
//                         {
//                            alert(9999999999);
//                            objTXT.style.borderColor = "Red";
//                         }
                        

                        //var rowsel =masterTableView.get_selectedItems()[txtIn];
                        //var NH_I1 =  masterTableView.getCellByColumnUniqueName(rowsel, "NH");
                        
                         //var allCells = rowsel.Control.cells[i];                 
                         //NH_I1.style.fontSize = "20px";           
                        // NH_I1.style.fontWeight = "Bold";      


                       //alert('hi');
                        for (var i = 0; i < selectedRows.length; i++) 
                        //for (var i = 0; i <1; i++) 
                       {      
                                  
                               var errmsg1=''; 
                               var validrecord ='true'
                               var starDate='';
                               var endDate='';
                               var errMessage='';
                               
                               var earyInTime='';
                               var lateIntime ='';
                               var earlyOutTime='';
                                
                               var row                  = selectedRows[i];                                
                               var cell                 = masterTableView.getCellByColumnUniqueName(row, "Err");                               
                               var inTimeCell           = masterTableView.getCellByColumnUniqueName(row, "InShortTime");
                               var outTimeCell          = masterTableView.getCellByColumnUniqueName(row, "OutShortTime");
                               var imgErrCell           = masterTableView.getCellByColumnUniqueName(row, "ErrImg");
                               
                               var sdateCell            = masterTableView.getCellByColumnUniqueName(row, "SDate");
                               var eDateCell            = masterTableView.getCellByColumnUniqueName(row, "EDate");
                               
                               var earyInTimeCell       =   masterTableView.getCellByColumnUniqueName(row, "EarlyInBy");
                               var lateIntimeCell       =   masterTableView.getCellByColumnUniqueName(row, "LateInBy");
                               var earlyOutTimeCell     =   masterTableView.getCellByColumnUniqueName(row, "EarlyOutBy");
                               
                               var errorNew             = masterTableView.getCellByColumnUniqueName(row, "ErrNew");
                               rosterType               = masterTableView.getCellByColumnUniqueName(row, "RosterType");
                               
                               var cell_SD          = masterTableView.getCellByColumnUniqueName(row, "SD");//SD drpSD                                       
                               var cell_ED          = masterTableView.getCellByColumnUniqueName(row, "ED");//SD drpSD
                               var cell_NewR        = masterTableView.getCellByColumnUniqueName(row, "NewR");//SD drpSD
                               
                               
                               //alert(cell1.innerText);
                               //alert(cell1.getElementsByTagName("input")[0].value);
                               
                               //var oldHTML = cell.innerHTML;//document.getElementById('para').innerHTML;                               
	                           //var newHTML = "<span style='color:#ffffff'>" + oldHTML + "</span>";
	                           //document.getElementById('para').innerHTML = newHTML;
	                           //alert(rosterType);
	                           //alert(cell_SD);
	                           //alert(cell_ED);
	                           
	                             
	                           
	                           var sdate =cell_SD.getElementsByTagName("SELECT")[0].value.split('/');
	                           var edate =cell_ED.getElementsByTagName("SELECT")[0].value.split('/');
	                           
	                           
	                           starDate             =   sdate[1]+'/' +sdate[0]+'/'+sdate[2] + ' ' + inTimeCell.getElementsByTagName("input")[0].value;
                               endDate              =   edate[1]+'/' +edate[0]+'/'+edate[2] + ' ' + outTimeCell.getElementsByTagName("input")[0].value;
                               
//                             earyInTime           =   cell_SD.getElementsByTagName("SELECT")[0].value + ' ' + earyInTimeCell.innerHTML;
//                             lateIntime           =   cell_SD.getElementsByTagName("SELECT")[0].value + ' ' + lateIntimeCell.innerHTML;
//                             earlyOutTime         =   cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + earlyOutTimeCell.innerHTML;
                                
                               earyInTime           =   sdate[1]+'/' +sdate[0]+'/'+sdate[2] + ' ' + earyInTimeCell.innerHTML;
                               lateIntime           =   sdate[1]+'/' +sdate[0]+'/'+sdate[2] + ' ' + lateIntimeCell.innerHTML;
                               earlyOutTime         =   edate[1]+'/' +edate[0]+'/'+edate[2] + ' ' + earlyOutTimeCell.innerHTML;  

                              
                                var SD1              =   cell_SD.getElementsByTagName("SELECT")[0].value;
                                var ED1              =   cell_ED.getElementsByTagName("SELECT")[0].value;
                              
                                var SD               =   new Date(starDate);  
                                var ED               =   new Date(endDate);
                                
                               var oneDay = 24*60*60*1000; // hours*minutes*seconds*milliseconds
                                //var firstDate = new Date(2008,01,12); 
                                //var secondDate = new Date(2008,01,22);   
                               var diffDays = Math.abs((SD.getTime() - ED.getTime()) / (oneDay)); 
                                                                                               
                               //var gridItem = gridItems[i];
                               // var cell =masterTableView.getCellByColumnUniqueName(row, "GridClientSelectColumn");// gridItem.get_cell("GridClientSelectColumn");
                               //var controlsArray = cell.getElementsByTagName('input');
                                                                                             
                               //SD  =starDate;  
                               //ED  =endDate;
                               //alert(dateBits[2]); 
                               //alert(dateBits[1]); 
                               //alert(dateBits[0]); 
                               //alert(hhmm[0]); 
                               //alert(hhmm[1]);
                               // alert(SD);
                               // alert(ED);

                               //*********************************************
                                var earyInDT         = new Date(earyInTime);
                                var lateInDT         = new Date(lateIntime);
                                var earlyOutDT       = new Date(earlyOutTime);
                               //*********************************************
	                          if(inTimeCell.getElementsByTagName("input")[0].value=='__:__' || inTimeCell.getElementsByTagName("input")[0].value=='')
                              {
                                      validrecord='false';
                                      msg='InTime Can not be blank';
                                      errorNew.innerText=1;
                              }
                                      
                             if(outTimeCell.getElementsByTagName("input")[0].value=='__:__' || outTimeCell.getElementsByTagName("input")[0].value=='')
                             {
                                //validrecord='false';
                                //msg='OutTime Can not be blank';
                                //errorNew.innerText=1;
                                //outTimeCell.getElementsByTagName("input")[0].value=inTimeCell.getElementsByTagName("input")[0].value;
                             } 
                         
                             
                                                                 if(rosterType.innerHTML=='FLEXIBLE')
                                                                 {
                                                                           
                                                                            if(SD1==ED1)
                                                                            {
                                                                                 if(validrecord=='true')
                                                                                  {
                                                                                     if (Date.parse(SD) > Date.parse(ED)) 
                                                                                     {  
                                                                                         //validrecord='false';
                                                                                         //msg='Start time can not be greater than out time';
                                                                                         //errorNew.innerText=1;
                                                                                     }                                                                                               
                                                                                 }                                             
                                                                            }
                                                                            else if(SD1!=ED1) //NightShift
                                                                            {
                                                                                 if(validrecord=='true')
                                                                                  {
                                                                                        if (Date.parse(SD) > Date.parse(ED)) 
                                                                                        {  
                                                                                            validrecord='false';
                                                                                            msg='Start time can not be greater than out time.If Records are for two days';
                                                                                            errorNew.innerText=1;
                                                                                        }                                                    
                                                                                  }                                 
                                                                            }
                                                                   }
                                                                   else                               
                                                                   {       
                                                                          if(SD1==ED1)//Same Day
                                                                            {
                                                                                 if(validrecord=='true')
                                                                                  {
                                                                                     if (Date.parse(SD) > Date.parse(ED)) 
                                                                                     {  
                                                                                         //validrecord='false';
                                                                                         //msg='Start time can not be greater than out time';
                                                                                         //errorNew.innerText=1;                                                                                                                                                                               
                                                                                     }                                                 
                                                                                     //Check In Time is less than Early In Time Of roster ....                                                                                              
                                                                                    if ((Date.parse(SD) < Date.parse(earyInDT))  && validrecord=='true')
                                                                                    {  
                                                                                        if(cell_NewR.innerText=='N')
                                                                                        {
                                                                                            validrecord='false';
                                                                                            msg='Start time can not be Less than Early in time Of Roster';
                                                                                            errorNew.innerText=1;
                                                                                        }                                                                                         
                                                                                    }                                               
                                                                                    
                                                                                    //Check In Time is Greater  than Late In Time Of roster ....
                                                                                    if ((Date.parse(SD) > Date.parse(lateInDT)) && validrecord=='true')
                                                                                    {  
                                                                                         validrecord='false';
                                                                                         msg='Start time can not be Greater than Late In Time of Roster';
                                                                                         errorNew.innerText=2;
                                                                                    }
                                                                                                                               
                                                                                    //Check out time not less than earlyOutDT Early out time of roster
                                                                                    //alert(earlyOutTime.format("mm/dm/yyyy HH:MM:SS"));
                                                                                    //alert(earlyOutTime);
                                                                                    //alert(ED);
                                                                                    if ((Date.parse(ED) < Date.parse(earlyOutDT))&& validrecord=='true')
                                                                                    {  
                                                                                        validrecord='false';
                                                                                        msg='End time can not be Less than EarlyOut Time of Roster';
                                                                                        errorNew.innerText=2;
                                                                                    }
                                                                                 }    
                                                                                 //Check if same end date is there .........
                                                                            }
                                                                            else if(SD1!=ED1)//NightShift
                                                                            {
                                                                                 if(validrecord=='true')
                                                                                  {
                                                                                        if (Date.parse(SD) > Date.parse(ED))
                                                                                        {  
                                                                                                validrecord='false';
                                                                                                msg='Start time can not be greater than out time.If Records are for two days';
                                                                                                errorNew.innerText=1;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                                validrecord='true';
                                                                                        }
                                                                                        
                                                                                         //Check In Time is less than Early In Time Of roster ....                                                                                              
                                                                                        if ((Date.parse(SD) < Date.parse(earyInDT))  && validrecord=='true')
                                                                                        {  
                                                                                             if(cell_NewR.innerText=='N')
                                                                                             {
                                                                                                    validrecord='false';
                                                                                                    msg='Start time can not be Less than Early in time Of Roster';
                                                                                                    errorNew.innerText=1;
                                                                                             }
                                                                                        }                                               
                                                                                        
                                                                                        //Check In Time is Greater  than Late In Time Of roster ....
                                                                                        if ((Date.parse(SD) > Date.parse(lateInDT)) && validrecord=='true')
                                                                                        {  
                                                                                             if(cell_NewR.innerText=='N')
                                                                                             {
                                                                                                validrecord='false';
                                                                                                msg='Start time can not be Greater than Late In Time of Roster';
                                                                                             }
                                                                                             errorNew.innerText=2;
                                                                                        }
                                                                                                                                    
                                                                                        //Check out time not less than earlyOutDT Early out time of roster
                                                                                        //alert(ED);
                                                                                        //alert(earlyOutDT);
                                                                                        //alert(ED);
                                                                                        
                                                                                        if ((Date.parse(ED) < Date.parse(earlyOutDT))&& validrecord=='true')
                                                                                        {  
                                                                                            if(cell_NewR.innerText=='N')
                                                                                            {
                                                                                                validrecord='false';
                                                                                                msg='End time can not be Less than EarlyOut Time of Roster';
                                                                                                errorNew.innerText=2;
                                                                                             }
                                                                                        }
                                                                                  }
                                                                            }
                                                                   }
                                     
                             
                               if(validrecord=='false')
                               {
                                    if(errorNew.innerText=='2')
                                    {
                                        imgErrCell.getElementsByTagName("IMG")[0].title =   msg;
                                        imgErrCell.getElementsByTagName("IMG")[0].src   =  newURL + "/Frames/Images/STATUSBAR/WAR.png";
                                        
                                            //Frames\Images\Other\ModalDialogAlert.gif
                                            var diff                =  timeHourDifferenceCD(SD,ED);
                                            
                                            var NH_I                =  masterTableView.getCellByColumnUniqueName(row, "NH");
                                            var NHWHM_I             =  masterTableView.getCellByColumnUniqueName(row, "NHWHM");
                                            var OT1_I               =  masterTableView.getCellByColumnUniqueName(row, "OT1");
                                            var BKOT1_I             =  masterTableView.getCellByColumnUniqueName(row, "BKOT1");
                                            var OT2_I               =  masterTableView.getCellByColumnUniqueName(row, "OT2");
                                            var TWH_I               =  masterTableView.getCellByColumnUniqueName(row, "TWH");
                                            var WHACT_I             =  masterTableView.getCellByColumnUniqueName(row, "WHACT");
                                            var NHWHREM             =  masterTableView.getCellByColumnUniqueName(row, "NHWHREM");
                                            //alert(NHWHREM);
                                            WHACT_I.innerText       =   parseInt(diff);
                                            
                                            var BTOTHR_I            =  masterTableView.getCellByColumnUniqueName(row, "BTOTHR");
                                            var BTNHHT_I            =  masterTableView.getCellByColumnUniqueName(row, "BTNHHT");                                            
                                            var BreakTimeNH_I       =  masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH");                                            
                                            
                                            
                                            var sdate =cell_SD.getElementsByTagName("SELECT")[0].value.split('/');
	                                        var edate =cell_ED.getElementsByTagName("SELECT")[0].value.split('/');
	                           
	                           
	                                        starDate             =   sdate[1]+'/' +sdate[0]+'/'+sdate[2] + ' ' + inTimeCell.getElementsByTagName("input")[0].value;
	                                        
//                                            if(outTimeCell.getElementsByTagName("input")[0].value=='__:__' || outTimeCell.getElementsByTagName("input")[0].value=='')
//                                            {
//                                                //validrecord='false';
//                                                //msg='OutTime Can not be blank';
//                                                //errorNew.innerText=1;
//                                                outTimeCell.getElementsByTagName("input")[0].value=inTimeCell.getElementsByTagName("input")[0].value;
//                                            }
                                            
                                            endDate              =   edate[1]+'/' +edate[0]+'/'+edate[2] + ' ' + outTimeCell.getElementsByTagName("input")[0].value;
                               
                               
                                            // earyInTime           =   cell_SD.getElementsByTagName("SELECT")[0].value + ' ' + earyInTimeCell.innerHTML;
                                            // lateIntime           =   cell_SD.getElementsByTagName("SELECT")[0].value + ' ' + lateIntimeCell.innerHTML;
                                            // earlyOutTime         =   cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + earlyOutTimeCell.innerHTML;

                                           // var BTOTDATET           =   cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + BTOTHR_I.innerHTML;
                                           // var BTNHDATET           =   cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + BTNHHT_I.innerHTML;                                            
                                           
                                            var BTOTDATET           =   sdate[1]+'/' +sdate[0]+'/'+sdate[2]+ ' ' + BTOTHR_I.innerHTML;
                                            var BTNHDATET           =   edate[1]+'/' +edate[0]+'/'+edate[2] + ' ' + BTNHHT_I.innerHTML;

                                            var NHDTBT              = new Date(BTNHDATET);
                                            var OTDTBT              = new Date(BTOTDATET);
                                            //TWH                                            
                                               //if(parseInt(NHWHM_I.innerText)<= parseInt(diff))
                                                if(parseInt(NHWHM_I.innerText)+ parseInt( BreakTimeNH_I.innerText)<= parseInt(diff))
                                                {
                                                    NH_I.innerText      =  parseInt(NHWHM_I.innerText); 
                                                   
                                                    if(( Date.parse(ED) >Date.parse(OTDTBT))|| rosterType.innerHTML=='FLEXIBLE')
                                                    {
                                                        OT1_I.innerText     =  parseInt(diff) -  parseInt(NHWHM_I.innerText) - parseInt(BKOT1_I.innerText)- parseInt(BreakTimeNH_I.innerText);                                                       
                                                    }    
                                                    else
                                                    {   
                                                        OT1_I.innerText     =  parseInt(diff) -  parseInt(NHWHM_I.innerText)- parseInt(BreakTimeNH_I.innerText);                                                    
                                                    }                                                    
                                                    if(parseInt(OT1_I.innerText)<0)
                                                    {
                                                                               
                                                        OT1_I.innerText =0;
                                                    }                                                    
                                                }
                                                else
                                                {
                                                     //NH_I.innerText  = parseInt(diff);// parseInt(diff)-parseInt(NHWHM_I.innerText);
                                                     //OT1_I.innerText =0;
                                                                                                   
                                                     OT1_I.innerText     =   0;               
                                                     NH_I.innerText      =   parseInt(diff);// parseInt(diff)-parseInt(NHWHM_I.innerText);
                                                    // alert(2);
                                                     if( Date.parse(ED) >Date.parse(NHDTBT)  )
                                                     {
                                                        NH_I.innerText = parseInt(diff) - parseInt(BreakTimeNH_I.innerText);
                                                     }
                                                     if(SD1!=ED1 &&  Date.parse(ED) <Date.parse(NHDTBT))
                                                     {
                                                        NH_I.innerText = parseInt(diff) - parseInt(BreakTimeNH_I.innerText);
                                                     }
                                                }
                                                TWH_I.innerText = parseInt(NH_I.innerText)+ parseInt(OT1_I.innerText);
                                                
                                                //alert(NHWHREM.innerText);
                                                //NHWHREM.SetAttribute("input",10);
                                                if(cell_NewR.innerText=='N')
                                                {
                                                    //alert(cell_NewR.innerText);
                                                    NHWHREM.getElementsByTagName("input")[0].value=Math.abs(parseInt(NH_I.innerText)- parseInt(NHWHM_I.innerText)); 
                                                }
                                                //NHWHREM.innerText = Math.abs(parseInt(NH_I.innerText)- parseInt(NHWHM_I.innerText)); 
                                      }
                                    else
                                    {
                                        imgErrCell.getElementsByTagName("IMG")[0].title =   msg;
                                        imgErrCell.getElementsByTagName("IMG")[0].src   =  newURL + "/Frames/Images/STATUSBAR/alert4.png";
                                        
                                        var NH_I                =  masterTableView.getCellByColumnUniqueName(row, "NH");
                                        var OT1_I               =  masterTableView.getCellByColumnUniqueName(row, "OT1");
                                        var OT2_I               =  masterTableView.getCellByColumnUniqueName(row, "OT2");
                                        var TWH_I               =  masterTableView.getCellByColumnUniqueName(row, "TWH");
                                        var WHACT_I             =  masterTableView.getCellByColumnUniqueName(row, "WHACT");
                                       
                                        WHACT_I.innerText         =parseInt(0);
                                        
                                        NH_I.innerText = parseInt(0);
                                        OT1_I.innerText = parseInt(0);
                                        OT2_I.innerText = parseInt(0);
                                        TWH_I.innerText = parseInt( NH_I.innerText)+ parseInt( OT1_I.innerText);
                                        //Frames\Images\Other\ModalDialogAlert.gif
                                        //alert(2);
                                        
                                    }
                                    //var p = timeHourDifference(ED,SD); 
                                    cnt=cnt+1;
                               }
                               else if(validrecord=='true')// && imgErrCell.getElementsByTagName("IMG")[0].src!= newURL + "/Frames/Images/STATUSBAR/Error.png")
                               {
                                    //alert(1);
                                    imgErrCell.getElementsByTagName("IMG")[0].src=  newURL + "/Frames/Images/STATUSBAR/OK.png";
                                    imgErrCell.title ='valid record'; 
                                   
                                   
                                     var InTime   =  masterTableView.getCellByColumnUniqueName(row, "InTime");
                                     
                                     var starDate1  ='';
                                     var cell_SD1          =   masterTableView.getCellByColumnUniqueName(row, "SD");
                                     var sdate1            =   cell_SD1.getElementsByTagName("SELECT")[0].value.split('/');
                                     starDate1             =   sdate1[1]+'/'+sdate1[0]+'/'+sdate1[2] + ' ' + InTime.innerText;
	                                 var SD1               =   new Date(starDate1);  
	                                 
                                     if ((Date.parse(SD1)> Date.parse(SD)))
                                     { 
                                         if(rosterType.innerHTML!='FLEXIBLE')                               
                                         {
                                             SD=SD1;    
                                         }                                   
                                     }

                                     //SD=SD1;
                                    
                                    //var diff              = timeHourDifference(ED,SD);    
                                    var diff                = timeHourDifferenceCD(SD,ED);    
                                    
                                   
                                    
                                    var NH_I               =  masterTableView.getCellByColumnUniqueName(row, "NH");
                                    var NHWHM_I            =  masterTableView.getCellByColumnUniqueName(row, "NHWHM");
                                                                        
                                    var OT1_I              =  masterTableView.getCellByColumnUniqueName(row, "OT1");
                                    var OT2_I              =  masterTableView.getCellByColumnUniqueName(row, "OT2");
                                    var BKOT1_I            =  masterTableView.getCellByColumnUniqueName(row, "BKOT1");                                    
                                    var WHACT_I            =  masterTableView.getCellByColumnUniqueName(row, "WHACT");
                                    
                                    WHACT_I.innerText      =  parseInt(diff);
                                    var NHWHREM             =  masterTableView.getCellByColumnUniqueName(row, "NHWHREM");
                                   
                                    var BTOTHR_I            =  masterTableView.getCellByColumnUniqueName(row, "BTOTHR");
                                    var BTNHHT_I            =  masterTableView.getCellByColumnUniqueName(row, "BTNHHT");                                            
                                    var BreakTimeNH_I       =  masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH");                                            
                                            
                                    var BTOTDATET           =  cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + BTOTHR_I.innerHTML;
                                    var BTNHDATET           =  cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + BTNHHT_I.innerHTML;
                                    
                                    var NHDTBT              = new Date(BTNHDATET);
                                    var OTDTBT              = new Date(BTOTDATET);         
                                            
                                    //alert(NHWHM_I.innerText);
                                    
                                    var TWH_I              =  masterTableView.getCellByColumnUniqueName(row, "TWH");
                                    
                                    //alert(parseInt(NHWHM_I.innerText));
                                    //alert(parseInt(diff));
                                      if(diffDays=='1')
                                      {
                                        diff ='1440'
                                      }
                                      
                                      if(diffDays>1)
                                      {
                                            var num = diffDays - parseInt(diffDays);
                                            if(num=='0')
                                            {
                                                diff = diffDays * 24 *60;
                                            }
                                            else
                                            {
                                                diff = diffDays * 24 *60;
                                                diff =diff + num;    
                                            }
                                      }
                                      
                                    if(parseInt(NHWHM_I.innerText)+ parseInt( BreakTimeNH_I.innerText)<= parseInt(diff))
                                    {
                                          NH_I.innerText      =  parseInt(NHWHM_I.innerText);
                                          //alert(223333);                                      
//                                        if( Date.parse(ED) >Date.parse(NHDTBT))
//                                        {
//                                                NH_I.innerText = parseInt(diff) - BreakTimeNH_I.innerText;
//                                        }
//                                        
                                        
                                        if( Date.parse(ED) >Date.parse(OTDTBT) || (rosterType.innerHTML=='FLEXIBLE'))
                                        {
                                            OT1_I.innerText     =  parseInt(diff) -  parseInt(NHWHM_I.innerText) - parseInt(BKOT1_I.innerText)- parseInt(BreakTimeNH_I.innerText);
                                            
                                        }    
                                        else
                                        {   
                                            //OT1_I.innerText     =  parseInt(diff) -  parseInt(NHWHM_I.innerText)- parseInt(BreakTimeNH_I.innerText);
                                            //alert( parseInt(BreakTimeNH_I.innerText));
                                            //OT1_I.innerText=parseInt(diff) -  parseInt(NHWHM_I.innerText);
                                            OT1_I.innerText     =  parseInt(diff) -  parseInt(NHWHM_I.innerText) - parseInt(BKOT1_I.innerText)- parseInt(BreakTimeNH_I.innerText);
                                        }
                                        if(parseInt(OT1_I.innerText)<0)
                                        {
                                                OT1_I.innerText=0;
                                        }
                                        
                                    //Check for Multiple Days i.e Number of hours>24                                        
                                    //alert(diffDays);
                                    //alert(999999);
                                    }
                                    else
                                    {
                                         //NH_I.innerText  = parseInt(diff);// parseInt(diff)-parseInt(NHWHM_I.innerText);
                                         //OT1_I.innerText =0;
                                         //alert(111111111111);
                                          //alert(7770000);
                                         OT1_I.innerText     = 0;
                                         NH_I.innerText = parseInt(diff); // parseInt(diff)-parseInt(NHWHM_I.innerText);

                                         if(rosterType.innerHTML=='FLEXIBLE')
                                         {
                                            //Get Value for BreakTimeAfter
                                            //var BreakTime_After =masterTableView.getCellByColumnUniqueName(row, "BreakTimeAfter");
                                            var breakTimeAfter = masterTableView.getCellByColumnUniqueName(row, "BreakTimeAfter").innerText;
                                            
                                            if(breakTimeAfter=='')
                                            {
                                                breakTimeAfter ='0';
                                            }
                                            //alert(parseInt(diff));
                                            
//                                            if(parseInt(diff)>=240 && parseInt(diff)<(240 + parseInt(BreakTimeNH_I.innerText)))
//                                            {
//                                                 NH_I.innerText=240;
//                                                
//                                            }
//                                            else if(parseInt(diff)<240)
//                                            {
//                                                 NH_I.innerText=parseInt(diff);
//                                            
//                                            }
//                                            else
//                                            {
//                                                NH_I.innerText=parseInt(diff)- parseInt(BreakTimeNH_I.innerText);
//                                            }
//                          alert(1); 
                                            if(parseInt(diff)>=parseInt(breakTimeAfter) && parseInt(diff)<(parseInt(breakTimeAfter) + parseInt(BreakTimeNH_I.innerText)))
                                            {
                                                 NH_I.innerText=parseInt(breakTimeAfter);
                                            }
                                            else if(parseInt(diff)<parseInt(breakTimeAfter))
                                            {
                                                 NH_I.innerText=parseInt(diff);
                                            
                                            }
                                            else
                                            {
                                                NH_I.innerText=parseInt(diff)- parseInt(BreakTimeNH_I.innerText);
                                            }   

                                            
//                                            else if(parseInt(diff)<240)
//                                            {
//                                                 NH_I.innerText =parseInt(diff);
//                                            }
//                                            else
//                                            {
//                                            
//                                                  NH_I.innerText= parseInt(diff);
//                                            }
                                         }
                                          
                                         if( Date.parse(ED) >Date.parse(NHDTBT))
                                         {
                                            NH_I.innerText = parseInt(diff) - parseInt(BreakTimeNH_I.innerText);
                                            //alert('hi');
                                            //alert(parseInt(BreakTimeNH_I.innerText));
                                         }
                                    }
                                    TWH_I.innerText = parseInt(NH_I.innerText)+ parseInt(OT1_I.innerText);
                                    //NHWHREM.innerText// =Math.abs(parseInt(NH_I.innerText)- parseInt(NHWHM_I.innerText)); 
//                                     alert(NHWHREM.getElementsByTagName("input")[0].value);
//                                     if(parseInt(NHWHREM.getElementsByTagName("input")[0].value)>0)
//                                     {
//                                     
//                                     }
                                    if(cell_NewR.innerText=='N')
                                    {  
                                        //alert(cell_NewR.innerText);
                                        NHWHREM.getElementsByTagName("input")[0].value=Math.abs(parseInt(NH_I.innerText)- parseInt(NHWHM_I.innerText)); 
                                    }
                               }
                               //here cell.innerHTML holds the value of the cell    
                        }   
                        
                        var chkrecordsobj = document.getElementById('chkrecords');
                        var chkbox =chkrecordsobj.childNodes[0].childNodes[0].childNodes[0].childNodes[0].checked;
                        //For 1 Day Two Entries
                        //selectedRows = masterTableView.get_selectedItems(); 
                        //alert('ddd');
                        //Day Shift 
                        if(chkbox==false)
                        {
                                        for (var i = 0; i < selectedRows.length; i++) 
                                        {        
                                               var errmsg1=''; 
                                               var validrecord ='true'
                                               var starDate='';
                                               var endDate='';
                                               var errMessage='';                               
                                               var earyInTime='';
                                               var lateIntime ='';
                                               var earlyOutTime='';
                                                
                                               var row                  =   selectedRows[i];                                
                                               var cell                 =   masterTableView.getCellByColumnUniqueName(row, "Err");                               
                                               var inTimeCell           =   masterTableView.getCellByColumnUniqueName(row, "InShortTime");
                                               var outTimeCell          =   masterTableView.getCellByColumnUniqueName(row, "OutShortTime");
                                               var imgErrCell           =   masterTableView.getCellByColumnUniqueName(row, "ErrImg");
                                               var rosterType           =   masterTableView.getCellByColumnUniqueName(row, "RosterType");
                                                                              
                                               var sdateCell            =   masterTableView.getCellByColumnUniqueName(row, "SDate");
                                               var eDateCell            =   masterTableView.getCellByColumnUniqueName(row, "EDate");
                                               
                                               var errorNew             =   masterTableView.getCellByColumnUniqueName(row, "ErrNew");                               
                                               rosterType               =   masterTableView.getCellByColumnUniqueName(row, "RosterType");
                                               
                                               var cell_SD              =   masterTableView.getCellByColumnUniqueName(row, "SD");   //SD drpSD                                       
                                               var cell_ED              =   masterTableView.getCellByColumnUniqueName(row, "ED");   //SD drpSD
                                               
	                                           starDate                 =   cell_SD.getElementsByTagName("SELECT")[0].value + ' ' + inTimeCell.getElementsByTagName("input")[0].value;
                                               endDate                  =   cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + outTimeCell.getElementsByTagName("input")[0].value;                               
                                               
                                               var TSSDDATE_O           =   cell_SD.getElementsByTagName("SELECT")[0].value;        //Time Sheet DateTime In 
                                               var ED1                  =   cell_ED.getElementsByTagName("SELECT")[0].value;        //TimeSheet Date time end                              
                                               var DATETIME_O_IN        =   new Date(starDate);             //IN Datetime Outer
                                               var DATETIME_O_OUT       =   new Date(endDate);              //OUT Datetime outer
                                               var NHRemaining=0;  
                                               var OTRotal=0;   
                                               var NHWorked =0;
                                               
                                                    //INNER FOR LOOP                                    
                                                    for (var j = 0; j < selectedRows.length; j++)
                                                    { 
                                                            var rowInner= selectedRows[j];
                                                            
                                                            var cell_SD_In         =  masterTableView.getCellByColumnUniqueName(rowInner, "SD");//SD drpSD                                       
                                                            var cell_ED_In         =  masterTableView.getCellByColumnUniqueName(rowInner, "ED");//SD drpSD
                                                            
                                                            var inTimeCell_In      =  masterTableView.getCellByColumnUniqueName(rowInner, "InShortTime");
                                                            var outTimeCell_In     =  masterTableView.getCellByColumnUniqueName(rowInner, "OutShortTime");
                                                            
                                                            var starDate_I         =  cell_SD_In.getElementsByTagName("SELECT")[0].value + ' ' + inTimeCell_In.getElementsByTagName("input")[0].value;
                                                            var endDate_I          =  cell_ED_In.getElementsByTagName("SELECT")[0].value + ' ' + outTimeCell_In.getElementsByTagName("input")[0].value;
                                                            
                                                            var TSSDDATE_I         =  cell_SD_In.getElementsByTagName("SELECT")[0].value; //Time Sheet DateTime In 
                                                            var TSEDDATE_I         =  cell_ED_In.getElementsByTagName("SELECT")[0].value; //TimeSheet Date time end                              
                                                            var DATETIME_I_IN      =  new Date(starDate_I);//IN Datetime Outer
                                                            var DATETIME_I_OUT     =  new Date(endDate_I);//OUT Datetime outer
                                                            var imgErrCell_I       =  masterTableView.getCellByColumnUniqueName(rowInner, "ErrImg");
                                                            var errorNew_I         =  masterTableView.getCellByColumnUniqueName(rowInner, "ErrNew");
                                                            var NHWHM_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
                                                            
                                                             
                                                            //if(((TSSDDATE_O==TSSDDATE_I)&&(i!=j)&&(j>i)) ||((ED1==TSSDDATE_I)&&(i!=j)&&(j>i)))
                                                            //alert(TSSDDATE_O);
                                                            //alert(TSSDDATE_I);
                                                            //if((TSSDDATE_O==TSSDDATE_I)&&(i!=j)&&(j>i))
//                                                            if((TSSDDATE_O==TSSDDATE_I)&&(i!=j)&&(j>i))
//                                                            {
//                                                                    if((Date.parse(DATETIME_I_IN) <Date.parse(DATETIME_O_OUT)))
//                                                                    {
//                                                                            //flag='false';
//                                                                            //imgErrCell_I.getElementsByTagName("IMG")[0].src=  newURL + "/Frames/Images/STATUSBAR/alert4.png";
//                                                                            //imgErrCell_I.getElementsByTagName("IMG")[0].title = 'Time conflict';
//                                                                            //errorNew_I.innerText=3;
//                                                                            
//                                                                            var NH_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
//                                                                            var OT1_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1");
//                                                                            var OT2_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT2");
//                                                                            var TWH_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "TWH");
//                                                                            var WHACT_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
//                                                                            
//                                                                            WHACT_I.innerText      =  parseInt(0);
//                                                                                
//                                                                            NH_I.innerText         =   parseInt(0);
//                                                                            OT1_I.innerText        =   parseInt(0);
//                                                                            OT2_I.innerText        =   parseInt(0);
//                                                                            TWH_I.innerText        =   parseInt(0);
//                                                                            
//                                                                    }
//                                                                    else if(imgErrCell_I.getElementsByTagName("IMG")[0].src !=  newURL + "/Frames/Images/STATUSBAR/alert4.png")
//                                                                    {
//                                                                            
//                                                                            imgErrCell_I.getElementsByTagName("IMG")[0].src=  newURL + "/Frames/Images/STATUSBAR/OK.png";
//                                                                            imgErrCell_I.getElementsByTagName("IMG")[0].title = 'Valid Record';
//                                                                            errorNew_I.innerText=0;
//                                                                            
//                                                                            
//                                                                            var WHACT_I   =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
//                                                                            //var diff = timeHourDifference(DATETIME_I_OUT,DATETIME_I_IN); 
//                                                                            
//                                                                            var diff = timeHourDifferenceCD(DATETIME_I_IN,DATETIME_I_OUT); 
//                                                                            
//                                                                            
//                                                                            
//                                                                            var NH_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
//                                                                            var NHWHM_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
//                                                                            
//                                                                            var OT1_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1");
//                                                                            var OT2_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT2");
//                                                                            var BKOT1_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "BKOT1");
//                                                                            
//                                                                            var WHACT_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
//                                                                            WHACT_I.innerText      =  parseInt(diff);
//                                                                                    
//                                                                                // New code starts here
//                                                                                var NH_I                =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
//                                                                                var NHWHM_I             =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
//                                                                                var OT1_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1");
//                                                                                var BKOT1_I             =  masterTableView.getCellByColumnUniqueName(rowInner, "BKOT1");
//                                                                                var OT2_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "OT2");
//                                                                                var TWH_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "TWH");
//                                                                                var WHACT_I             =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
//                                                                                var NHWHREM             =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHREM");
//                                                                                //alert(NHWHREM);
//                                                                                WHACT_I.innerText       =  parseInt(diff);
//                                                                                //NHWHM_I.innerText       =  NHWHREM.getElementsByTagName("input")[0].value;
//                                                                                
//                                                                                var BTOTHR_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "BTOTHR");
//                                                                                var BTNHHT_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "BTNHHT");                                            
//                                                                                var BreakTimeNH_I       =  masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH");                                            
//                                                                                
//                                                                                var BTOTDATET           =   cell_SD_In.getElementsByTagName("SELECT")[0].value + ' ' + BTOTHR_I.innerHTML;
//                                                                                var BTNHDATET           =   cell_ED_In.getElementsByTagName("SELECT")[0].value + ' ' + BTNHHT_I.innerHTML;
//                                                                                var NHDTBT              =   new Date(BTNHDATET);
//                                                                                var OTDTBT              =   new Date(BTOTDATET);
//                                                                                
//                                                                                var breakTimeAfter = masterTableView.getCellByColumnUniqueName(row, "BreakTimeAfter").innerText;
//                                                                                 
//                                                                                    //alert( parseInt(NHRemaining));
//                                                                              //alert('IL1');
//                                                                                //alert(NHWHM_I.innerText);
//                                                                                
//                                                                                
//                                                                                //alert(parseInt(NHRemaining));
//                                                                                //TWH                                            
//                                                                                    //if(parseInt(NHWHM_I.innerText)<= parseInt(diff))
//                                                                                    
//                                                                                    if(parseInt(NHRemaining)<= parseInt(diff))
//                                                                                    {
//                                                                                            
//                                                                                         if((NHWorked+parseInt(NHRemaining))!=NHWHM_I.innerText )
//                                                                                         {
//                                                                                            NH_I.innerText      =  parseInt(NHRemaining);                                                                                            
//                                                                                           //alert(NHWorked);
//                                                                                           //alert(NHWHM_I.innerText);
//                                                                                           //alert('pppppppppppSSSSSSSSSSSSS');
//                                                                                           //alert(1);
//                                                                                         }
//                                                                                         else
//                                                                                         {
//                                                                                             NH_I.innerText      =  parseInt(NHRemaining);//- parseInt(BreakTimeNH_I.innerText);;
//                                                                                             
//                                                                                              //alert(2);
//                                                                                             
//                                                                                              //alert('ppppppppppp');
//                                                                                              if(rosterType.innerHTML=='FLEXIBLE')
//                                                                                              {
//                                                                                              //   NH_I.innerText      =  parseInt(NHRemaining)- parseInt(BreakTimeNH_I.innerText);
//                                                                                              }
//                                                                                             // alert(2);
//                                                                                         }
//                                                                                         
//                                                                                         if( parseInt(NH_I.innerText )<0)
//                                                                                         {
//                                                                                            NH_I.innerText =0;
//                                                                                         }
//                                                                                          
//                                                                                        if(( Date.parse(ED) >Date.parse(OTDTBT))|| rosterType.innerHTML=='FLEXIBLE')
//                                                                                        {
//                                                                                            
//                                                                                            if(OTRotal>BKOT1_I.innerText)
//                                                                                            {
//                                                                                                
//                                                                                                OT1_I.innerText =  parseInt(diff); //;-  parseInt(NHRemaining) ;//- parseInt(BreakTimeNH_I.innerText);
//                                                                                                //alert(1);
//                                                                                               //alert( parseInt(NHRemaining));
//                                                                                            }
//                                                                                            else
//                                                                                            {
//                                                                                                 
//                                                                                                 if(parseInt(NHRemaining)!=0)
//                                                                                                 {
//                                                                                                    
//                                                                                                    //OT1_I.innerText =  parseInt(diff) - parseInt(BKOT1_I.innerText) -   parseInt(NHRemaining)-parseInt(BreakTimeNH_I.innerText);;// - parseInt(BKOT1_I.innerText);//- parseInt(BreakTimeNH_I.innerText);
//                                                                                                    OT1_I.innerText =  parseInt(diff) - parseInt(NHRemaining);//-parseInt(BreakTimeNH_I.innerText);;// - parseInt(BKOT1_I.innerText);//- parseInt(BreakTimeNH_I.innerText);
//                                                                                                    //if(rosterType.innerHTML=='FLEXIBLE')
//                                                                                                    //{   
//                                                                                                        
//                                                                                                         if(breakTimeAfter<=NHRemaining)
//                                                                                                         {
//                                                                                                            OT1_I.innerText =  parseInt(diff) - parseInt(NHRemaining)-parseInt(BreakTimeNH_I.innerText);
//                                                                                                         }
//                                                                                                    //}
//                                                                                                 }
//                                                                                                 else
//                                                                                                 {
//                                                                                                    //alert(99999);
//                                                                                                    //OT1_I.innerText  =parseInt(diff);
//                                                                                                 }
//                                                                                                 
//                                                                                                 if( parseInt(NHRemaining)== parseInt(diff))
//                                                                                                 {
//                                                                                                    
//                                                                                                    OT1_I.innerText =parseInt(NHRemaining)- parseInt(diff);
//                                                                                                 }
//                                                                                                //alert(10);
//                                                                                                //alert(parseInt(NHWorked));                                                                                  
//                                                                                                //alert( parseInt(NHRemaining));
//                                                                                               //alert( parseInt(NHWHM_I.innerText));
//                                                                                                 if((NHWorked+parseInt(NHRemaining))==parseInt(NHWHM_I.innerText))
//                                                                                                 {
//                                                                                                    //alert(parseInt(diff)); 
//                                                                                                     //OT1_I.innerText =  parseInt(diff);//- parseInt(NH_I.innerText)- parseInt(BreakTimeNH_I.innerText);
//                                                                                                     //alert(20);
//                                                                                                     if(parseInt(OT1_I.innerText  )>parseInt(BKOT1_I.innerText))
//                                                                                                     {
//                                                                                                        //OT1_I.innerText =parseInt(OT1_I.innerText)-parseInt(BKOT1_I.innerText);
//                                                                                                      // alert(30);
//                                                                                                     }
//                                                                                                 }                                                                                                                                                                                  
//                                                                                            }                                                                                            
//                                                                                            if(parseInt(NHRemaining)==0)
//                                                                                            {
//                                                                                                 OT1_I.innerText     =  parseInt(diff);
//                                                                                            }
//                                                                                            if(parseInt(OT1_I.innerText)<0)
//                                                                                            {
//                                                                                               //OT1_I.innerText =0;
//                                                                                                OT1_I.innerText     =  parseInt(diff);
//                                                                                            }    
//                                                                                            
//                                                                                            if((parseInt(diff)>parseInt(NHRemaining)) &&  (rosterType.innerHTML=='FLEXIBLE'))
//                                                                                            {
//                                                                                                    
//                                                                                                 if(parseInt(breakTimeAfter)==parseInt(NHRemaining) )
//                                                                                                 {
//                                                                                                      OT1_I.innerText =  parseInt(diff)-parseInt(NH_I.innerText);                                                                                                      
//                                                                                                 }
//                                                                                            }                                                                                        
//                                                                                        }    
//                                                                                        else
//                                                                                        {   
//                                                                                            OT1_I.innerText=parseInt(diff)-parseInt(NHRemaining)-parseInt(BreakTimeNH_I.innerText);//- parseInt(BreakTimeNH_I.innerText);
//                                                                                            
//                                                                                            //alert(50);
//                                                                                            //alert(2);
//                                                                                            if(parseInt(OT1_I.innerText)<0)
//                                                                                            {
//                                                                                               
//                                                                                                OT1_I.innerText =0;
//                                                                                            } 
//                                                                                        }
//                                                                                    }
//                                                                                    else
//                                                                                    {
//                                                                                      
//                                                                                        
//                                                                                        if(parseInt(NHWHM_I.innerText )!= parseInt(NHWorked))
//                                                                                         {
//                                                                                         
//                                                                                                NH_I.innerText =parseInt(diff);                                                                                
//                                                                                                if( Date.parse(ED) >Date.parse(NHDTBT))
//                                                                                                {
//                                                                                                    NH_I.innerText = parseInt(diff) - BreakTimeNH_I.innerText;
//                                                                                                   
//                                                                                                }
//                                                                                                 OT1_I.innerText =0;
//                                                                                                //alert(70);
//                                                                                         }
//                                                                                         else
//                                                                                         {
//                                                                                              OT1_I.innerText = parseInt(diff);
//                                                                                              NH_I.innerText =0;
//                                                                                         }
//                                                                                    }
//                                                                                    TWH_I.innerText = parseInt( NH_I.innerText)+ parseInt(OT1_I.innerText);
//                                                                    }                                                
//                                                            }
                                                            
                                                             //if((TSSDDATE_O==TSSDDATE_I) || (ED1==TSSDDATE_I))
                                                             if((TSSDDATE_O==TSSDDATE_I))
                                                             {
                                                                    
                                                                    //alert('IL');
                                                                  var NH_I1                 =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
                                                                  var NHWHM_I1              =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
                                                                  var OT1_I1                =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1")
                                                                  var BreakTimeNH_I       =  masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH");
                                                                  //OT1_I.innerText
                                                                  var val=0;
                                                                  NHWorked = NHWorked +  parseInt(NH_I1.innerText);
                                                                  
                                                                  //alert(NHRemaining);
                                                                 
                                                                  if(NHRemaining==0)
                                                                  {
                                                                        if(parseInt(NH_I1.innerText)<parseInt(NHWHM_I1.innerText))
                                                                        {
                                                                            val = parseInt(NHWHM_I1.innerText) - parseInt(NH_I1.innerText);//-parseInt(BreakTimeNH_I.innerText);
                                                                            NHRemaining = val ;
                                                                            //alert('NHR1');
                                                                            //alert(NHRemaining);
                                                                        }
                                                                        else
                                                                        {
                                                                            val =parseInt(NH_I1.innerText) -parseInt(NHWHM_I1.innerText);
                                                                            NHRemaining =  val;
                                                                            //alert('NHR2');
                                                                        }
                                                                        
                                                                        if(OTRotal==0)
                                                                        {
                                                                            OTRotal =parseInt(OT1_I1.innerText)
                                                                        }
                                                                  }
                                                                  else 
                                                                  {
                                                                        
                                                                        if(parseInt(NH_I1.innerText)<parseInt(NHRemaining))
                                                                        {
                                                                            val = parseInt(NHRemaining) - parseInt(NH_I1.innerText);
                                                                            NHRemaining = val ;
                                                                            //alert('NHR3');
                                                                           // alert(NHRemaining);
                                                                        }
                                                                        else
                                                                        {
                                                                            val =parseInt(NH_I1.innerText) -parseInt(NHRemaining);
                                                                            NHRemaining =  val;
                                                                            //alert('NHR4');
                                                                        }
                                                                        if(OTRotal==0)
                                                                        {
                                                                            if(OTRotal!=parseInt(OT1_I1.innerText))
                                                                            {
                                                                                OTRotal =OTRotal + parseInt(OT1_I1.innerText)
                                                                            }
                                                                        }                                                        
                                                                  }
                                                             } 
                                                             
                                                           
                                                    }
                                                    NHRemaining=0;
                                                    NHWorked =0;
                                         }        
                        
                        }//Condition for Night shift
                        else
                        {
                                        //alert('night shift');
                                        for (var i = 0; i < selectedRows.length; i++) 
                                        {        
                                               var errmsg1=''; 
                                               var validrecord ='true'
                                               var starDate='';
                                               var endDate='';
                                               var errMessage='';                               
                                               var earyInTime='';
                                               var lateIntime ='';
                                               var earlyOutTime='';
                                                
                                               var row                  =   selectedRows[i];                                
                                               var cell                 =   masterTableView.getCellByColumnUniqueName(row, "Err");                               
                                               var inTimeCell           =   masterTableView.getCellByColumnUniqueName(row, "InShortTime");
                                               var outTimeCell          =   masterTableView.getCellByColumnUniqueName(row, "OutShortTime");
                                               var imgErrCell           =   masterTableView.getCellByColumnUniqueName(row, "ErrImg");
                                               var rosterType           =   masterTableView.getCellByColumnUniqueName(row, "RosterType");
                                                                              
                                               var sdateCell            =   masterTableView.getCellByColumnUniqueName(row, "SDate");
                                               var eDateCell            =   masterTableView.getCellByColumnUniqueName(row, "EDate");
                                               
                                               var errorNew             =   masterTableView.getCellByColumnUniqueName(row, "ErrNew");                               
                                               rosterType               =   masterTableView.getCellByColumnUniqueName(row, "RosterType");
                                               
                                               var cell_SD              =   masterTableView.getCellByColumnUniqueName(row, "SD");   //SD drpSD                                       
                                               var cell_ED              =   masterTableView.getCellByColumnUniqueName(row, "ED");   //SD drpSD
                                               
	                                           starDate                 =   cell_SD.getElementsByTagName("SELECT")[0].value + ' ' + inTimeCell.getElementsByTagName("input")[0].value;
                                               endDate                  =   cell_ED.getElementsByTagName("SELECT")[0].value + ' ' + outTimeCell.getElementsByTagName("input")[0].value;                               
                                               
                                               var TSSDDATE_O           =   cell_SD.getElementsByTagName("SELECT")[0].value;        //Time Sheet DateTime In 
                                               var ED1                  =   cell_ED.getElementsByTagName("SELECT")[0].value;        //TimeSheet Date time end                              
                                               
                                               var DATETIME_O_IN        =   new Date(starDate);             //IN Datetime Outer
                                               var DATETIME_O_OUT       =   new Date(endDate);              //OUT Datetime outer
                                               
                                               var NHRemaining=0;  
                                               var OTRotal=0;   
                                               var NHWorked =0;
                                                   
                                                    //INNER FOR LOOP                                    
                                                    for (var j = 0; j < selectedRows.length; j++)
                                                    { 
                                                            var rowInner= selectedRows[j];
                                                            
                                                            var cell_SD_In         =  masterTableView.getCellByColumnUniqueName(rowInner, "SD");//SD drpSD                                       
                                                            var cell_ED_In         =  masterTableView.getCellByColumnUniqueName(rowInner, "ED");//SD drpSD
                                                            
                                                            var inTimeCell_In      =  masterTableView.getCellByColumnUniqueName(rowInner, "InShortTime");
                                                            var outTimeCell_In     =  masterTableView.getCellByColumnUniqueName(rowInner, "OutShortTime");
                                                            
                                                            var starDate_I         =  cell_SD_In.getElementsByTagName("SELECT")[0].value + ' ' + inTimeCell_In.getElementsByTagName("input")[0].value;
                                                            var endDate_I          =  cell_ED_In.getElementsByTagName("SELECT")[0].value + ' ' + outTimeCell_In.getElementsByTagName("input")[0].value;
                                                            
                                                            var TSSDDATE_I         =  cell_SD_In.getElementsByTagName("SELECT")[0].value; //Time Sheet DateTime In 
                                                            var TSEDDATE_I         =  cell_ED_In.getElementsByTagName("SELECT")[0].value; //TimeSheet Date time end                              
                                                            var DATETIME_I_IN      =  new Date(starDate_I);//IN Datetime Outer
                                                            var DATETIME_I_OUT     =  new Date(endDate_I);//OUT Datetime outer
                                                            var imgErrCell_I       =  masterTableView.getCellByColumnUniqueName(rowInner, "ErrImg");
                                                            var errorNew_I         =  masterTableView.getCellByColumnUniqueName(rowInner, "ErrNew");
                                                            var NHWHM_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
                                                            var NewR_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "NewR");
                                                            
                                                             
                                                           //if(((TSSDDATE_O==TSSDDATE_I)&&(i!=j)&&(j>i)) ||((ED1==TSSDDATE_I)&&(i!=j)&&(j>i)&&(NewR_I.innerText=='Y')))
                                                            //if((TSSDDATE_O==TSSDDATE_I)&&(i!=j)&&(j>i))
                                                            if((ED1 ==  TSSDDATE_I)&&(i!=j)&&(j>i)&&(NewR_I.innerText=='Y'))
                                                            {
                                                               // alert(88);
                                                            
                                                                  if((Date.parse(DATETIME_I_IN) <Date.parse(DATETIME_O_OUT)))
                                                                    {
                                                                            //flag='false';        
                                                                            imgErrCell_I.getElementsByTagName("IMG")[0].src=  newURL + "/Frames/Images/STATUSBAR/alert4.png";
                                                                            imgErrCell_I.getElementsByTagName("IMG")[0].title = 'Time conflict';
                                                                            errorNew_I.innerText=3;
                                                                            
                                                                            var NH_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
                                                                            var OT1_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1");
                                                                            var OT2_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT2");
                                                                            var TWH_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "TWH");
                                                                            var WHACT_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
                                                                            
                                                                            WHACT_I.innerText      =  parseInt(0);
                                                                                
                                                                            NH_I.innerText         =   parseInt(0);
                                                                            OT1_I.innerText        =   parseInt(0);
                                                                            OT2_I.innerText        =   parseInt(0);
                                                                            TWH_I.innerText        =   parseInt(0);          
                                                                            
                                                                    }
                                                                    else if(imgErrCell_I.getElementsByTagName("IMG")[0].src !=  newURL + "/Frames/Images/STATUSBAR/alert4.png")
                                                                    {
                                                                            
                                                                            imgErrCell_I.getElementsByTagName("IMG")[0].src=  newURL + "/Frames/Images/STATUSBAR/OK.png";
                                                                            imgErrCell_I.getElementsByTagName("IMG")[0].title = 'Valid Record';
                                                                            errorNew_I.innerText=0;
                                                                            
                                                                            
                                                                            var WHACT_I   =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
                                                                            //var diff = timeHourDifference(DATETIME_I_OUT,DATETIME_I_IN); 
                                                                            
                                                                            var diff = timeHourDifferenceCD(DATETIME_I_IN,DATETIME_I_OUT); 
                                                                            
                                                                            
                                                                            
                                                                            var NH_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
                                                                            var NHWHM_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
                                                                            
                                                                            var OT1_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1");
                                                                            var OT2_I              =  masterTableView.getCellByColumnUniqueName(rowInner, "OT2");
                                                                            var BKOT1_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "BKOT1");
                                                                            
                                                                            var WHACT_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
                                                                            WHACT_I.innerText      =  parseInt(diff);
                                                                                    
                                                                                // New code starts here
                                                                                var NH_I                =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
                                                                                var NHWHM_I             =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
                                                                                var OT1_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1");
                                                                                var BKOT1_I             =  masterTableView.getCellByColumnUniqueName(rowInner, "BKOT1");
                                                                                var OT2_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "OT2");
                                                                                var TWH_I               =  masterTableView.getCellByColumnUniqueName(rowInner, "TWH");
                                                                                var WHACT_I             =  masterTableView.getCellByColumnUniqueName(rowInner, "WHACT");
                                                                                var NHWHREM             =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHREM");
                                                                                //alert(NHWHREM);
                                                                                WHACT_I.innerText       =  parseInt(diff);
                                                                                //NHWHM_I.innerText       =  NHWHREM.getElementsByTagName("input")[0].value;
                                                                                
                                                                                var BTOTHR_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "BTOTHR");
                                                                                var BTNHHT_I            =  masterTableView.getCellByColumnUniqueName(rowInner, "BTNHHT");                                            
                                                                                var BreakTimeNH_I       =  masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH");                                            
                                                                                
                                                                                var BTOTDATET           =   cell_SD_In.getElementsByTagName("SELECT")[0].value + ' ' + BTOTHR_I.innerHTML;
                                                                                var BTNHDATET           =   cell_ED_In.getElementsByTagName("SELECT")[0].value + ' ' + BTNHHT_I.innerHTML;
                                                                                var NHDTBT              =   new Date(BTNHDATET);
                                                                                var OTDTBT              =   new Date(BTOTDATET);
                                                                             
                                                                                //alert( parseInt(NHRemaining));                                                                              
                                                                                //alert(NHWHM_I.innerText);
                                                                                //alert(diff);
                                                                                //alert(parseInt(NHRemaining));
                                                                                //TWH                                            
                                                                                //if(parseInt(NHWHM_I.innerText)<= parseInt(diff))
                                                                                
                                                                                    //alert(parseInt(NHRemaining));
                                                                                    //alert(parseInt(diff));
                                                                                                                                                                       
                                                                                    if(parseInt(NHRemaining)<= parseInt(diff))
                                                                                    {
                                                                                        alert(6);
                                                                                         if((NHWorked+parseInt(NHRemaining))!=NHWHM_I.innerText )
                                                                                         {
                                                                                            NH_I.innerText      =  parseInt(NHRemaining);
                                                                                            
                                                                                            //alert(NHWHM_I.innerText);
                                                                                           // alert(4444444444);
                                                                                         }
                                                                                         else
                                                                                         {
                                                                                             NH_I.innerText      =  parseInt(NHRemaining)- parseInt(BreakTimeNH_I.innerText);;
                                                                                             // alert('hi2');
                                                                                         }
                                                                                         
                                                                                         if( parseInt(NH_I.innerText )<0)
                                                                                         {
                                                                                            NH_I.innerText =0;
                                                                                         }
                                                                                       alert(22221);
                                                                                        
                                                                                        if(( Date.parse(ED) >Date.parse(OTDTBT))|| rosterType.innerHTML=='FLEXIBLE')
                                                                                        {
                                                                                            alert(1);
                                                                                            if(OTRotal>BKOT1_I.innerText)
                                                                                            {
                                                                                                OT1_I.innerText =  parseInt(diff); //;-  parseInt(NHRemaining) ;//- parseInt(BreakTimeNH_I.innerText);                                                                                               
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                            
                                                                                                OT1_I.innerText =  parseInt(diff) - parseInt(BKOT1_I.innerText) -   parseInt(NHRemaining);// - parseInt(BKOT1_I.innerText);//- parseInt(BreakTimeNH_I.innerText);
                                                                                                //alert(101);
                                                                                                 if((NHWorked+parseInt(NHRemaining))==parseInt(NHWHM_I.innerText))
                                                                                                 {
                                                                                                    //alert(parseInt(diff)); 
                                                                                                     OT1_I.innerText =  parseInt(diff)- parseInt(NH_I.innerText);//- parseInt(BreakTimeNH_I.innerText);
                                                                                                    // alert(103);
                                                                                                     if(parseInt(OT1_I.innerText  )>parseInt(BKOT1_I.innerText))
                                                                                                     {
                                                                                                        OT1_I.innerText =parseInt(OT1_I.innerText)-parseInt(BKOT1_I.innerText);
                                                                                                        //alert(104);
                                                                                                     }
                                                                                                 }                                                                                 
                                                                                            }
                                                                                            alert(888);
                                                                                            if(parseInt(OT1_I.innerText)<0)
                                                                                            {
                                                                                                OT1_I.innerText     =  parseInt(diff);
                                                                                                //alert(105);
                                                                                            }
                                                                                            
                                                                                        }    
                                                                                        else
                                                                                        {   
                                                                                           
                                                                                            OT1_I.innerText     =  parseInt(diff) -  parseInt(NHRemaining);//- parseInt(BreakTimeNH_I.innerText);
                                                                                            alert(106);
                                                                                            //alert(2);
                                                                                            if(parseInt(OT1_I.innerText)<0)
                                                                                            {
                                                                                               
                                                                                                OT1_I.innerText =0;
                                                                                                
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if(parseInt(NHWHM_I.innerText )!= parseInt(NHWorked))
                                                                                         {
                                                                                         
                                                                                                NH_I.innerText      =   parseInt(diff);                                                                                
                                                                                                if( Date.parse(ED) >Date.parse(NHDTBT))
                                                                                                {
                                                                                                    NH_I.innerText = parseInt(diff) - BreakTimeNH_I.innerText;
                                                                                                    
                                                                                                   
                                                                                                }
                                                                                                 OT1_I.innerText =0;
                                                                                                 //alert(77777777777777);
                                                                                                
                                                                                         }
                                                                                         else
                                                                                         {
                                                                                              OT1_I.innerText = parseInt(diff);
                                                                                              //alert(107);
                                                                                              NH_I.innerText =0;
                                                                                         }
                                                                                    }
                                                                                    TWH_I.innerText = parseInt( NH_I.innerText)+ parseInt(OT1_I.innerText);
                                                                    }                                                
                                                            }
                                                            
                                                             //if((TSSDDATE_O==TSSDDATE_I) || ((ED1==TSSDDATE_I)&&((NewR_I.innerText=='Y'))))
                                                             //if((TSSDDATE_O==TSSDDATE_I))
                                                             //if(((ED1==TSSDDATE_I)&&((NewR_I.innerText=='Y'))))
                                                              //if((ED1==TSSDDATE_I)&&(i!=j)&&(j>i)&&(NewR_I.innerText=='Y'))
                                                              
                                                             //alert(ED1);
                                                            // alert(TSSDDATE_I);
                                                             //alert(NewR_I.innerText);
                                                             
                                                             //if((ED1==TSSDDATE_I)) 
                                                             if(true) 
                                                             {
                                                                  var NH_I1                 =  masterTableView.getCellByColumnUniqueName(rowInner, "NH");
                                                                  var NHWHM_I1              =  masterTableView.getCellByColumnUniqueName(rowInner, "NHWHM");
                                                                  var OT1_I1                =  masterTableView.getCellByColumnUniqueName(rowInner, "OT1")
                                                                  //OT1_I.innerText
                                                                  var val=0;                                                                  
                                                                  NHWorked                  = NHWorked +  parseInt(NH_I1.innerText);
                                                                    
                                                                  if(NHRemaining==0)
                                                                  {
                                                                        if(parseInt(NH_I1.innerText)<parseInt(NHWHM_I1.innerText))
                                                                        {
                                                                            val = parseInt(NHWHM_I1.innerText) - parseInt(NH_I1.innerText);
                                                                            NHRemaining = val ;                                                                            
                                                                            //alert(NH_I1.innerText);
                                                                            //alert(NH_I1.innerText);                                                                                                                                                        
                                                                        }
                                                                        else
                                                                        {
                                                                            val =parseInt(NH_I1.innerText) -parseInt(NHWHM_I1.innerText);
                                                                            NHRemaining =  val;
                                                                            //alert('NHR5');
                                                                        }
                                                                        
                                                                        if(OTRotal==0)
                                                                        {
                                                                            OTRotal =parseInt(OT1_I1.innerText)
                                                                        }
                                                                        
                                                                        
                                                                  }
                                                                  else 
                                                                  {
                                                                        if(parseInt(NH_I1.innerText)<parseInt(NHRemaining))
                                                                        {
                                                                            val = parseInt(NHRemaining) - parseInt(NH_I1.innerText);
                                                                            NHRemaining = val ;
                                                                            //alert('NHR6');
                                                                        }
                                                                        else
                                                                        {
                                                                            val =parseInt(NH_I1.innerText) -parseInt(NHRemaining);
                                                                            NHRemaining =  val;
                                                                            //alert('NHR7');
                                                                        }
                                                                        if(OTRotal==0)
                                                                        {
                                                                            if(OTRotal!=parseInt(OT1_I1.innerText))
                                                                            {
                                                                                OTRotal =OTRotal + parseInt(OT1_I1.innerText)
                                                                            }
                                                                        }                                                        
                                                                  }
                                                                  
                                                                  //alert(NHWorked);
                                                             } 
                                                             
                                                           
                                                    }
                                                    NHRemaining=0;
                                                    NHWorked =0;
                                         } 
                        
                        }//Condition for Night Ends
                        
                        
                        //Covert NH/OT1/OT2/TOTAL in to HR
                        //var Hours = Math.floor(350/60);
                                                
                        var NHTOTAL     =0;
                        var OT1TOTAL    =0;
                        var OT2TOTAL    =0;
                        var TTOTAL      =0;
                        var flag1       ='true';
                        var flag2       ='true';
                        
                          for (var i = 0; i < selectedRows.length; i++) 
                          {
                                 var row            =   selectedRows[i];  
                                 var NH             =  masterTableView.getCellByColumnUniqueName(row, "NH");
                                 var OT1            =  masterTableView.getCellByColumnUniqueName(row, "OT1");
                                 var OT2            =  masterTableView.getCellByColumnUniqueName(row, "OT2");
                                 var TWH            =  masterTableView.getCellByColumnUniqueName(row, "TWH");
                                 
                                 
                                 var NHA            =  masterTableView.getCellByColumnUniqueName(row, "NHA");                                 
                                 var OT1A           =  masterTableView.getCellByColumnUniqueName(row, "OT1A");
                                 var OT2A           =  masterTableView.getCellByColumnUniqueName(row, "OT2A");
                                 var TotalA         =  masterTableView.getCellByColumnUniqueName(row, "TotalA");
                                 
                                 var NHAT            =  masterTableView.getCellByColumnUniqueName(row, "NHAT");                                 
                                 var OT1AT           =  masterTableView.getCellByColumnUniqueName(row, "OT1AT");
                                 var OT2AT           =  masterTableView.getCellByColumnUniqueName(row, "OT2AT");
                                 var TotalAT         =  masterTableView.getCellByColumnUniqueName(row, "TotalAT");
                                 
                                 var imgErrCell_I   =  masterTableView.getCellByColumnUniqueName(row, "ErrImg");
                                 
                                 var cell           =  masterTableView.getCellByColumnUniqueName(row, "GridClientSelectColumn");
                                 
                                 var check =cell.children[0];
                                 if(cell.children[0].children[0]==null)
                                 {
                                    check='';
                                 }
                                 else
                                 {
                                    
                                    check =cell.children[0].children[0].disabled;
                                 
                                 }
                                 
                                 //alert(inTimeCell_In1.Controls[0]);
                                 //alert(NHAT);
                                 //alert(OT1AT);
                                 //alert(OT2AT);
                                 //alert(TotalAT);
                                 
                                 //0 NHAT,0 OT1AT,0 OT2AT,0 TotalAT
                                
                                 if(imgErrCell_I.getElementsByTagName("IMG")[0].src ==  newURL + "/Frames/Images/STATUSBAR/alert4.png")
                                 {
                                    if(check)
                                    {
                                    
                                    }
                                    else
                                    {
                                        flag1='false';                                    
                                    }
                                 }              
                              
                                 
                                 if(check)
                                 {
                                    if(flag2=='true')
                                    {
                                        
                                        flag2='false';
                                     }
                                 }   
                                
                                 
                                 var converHrd = masterTableView.getCellByColumnUniqueName(row, "TrHrs");
                                 /***********Convert Hrs As per settings ******************/
                                 //alert(converHrd.innerText);
                                 if(converHrd.innerText=='NH To OT1')
                                 {
                                    //OT1TOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);                                                                        
                                    OT1.innerText = parseInt(NH.innerText) + parseInt(OT1.innerText);     
                                    
                                    NH.innerText=0;                                        
                                    //NHTOTAL=0;
                                     TWH.innerText =OT1.innerText ;   
                                                                 
                                 }
                                 
                                 if(converHrd.innerText=='NH To OT2')
                                 {
                                    //OT2TOTAL = parseInt(NHTOTAL);
                                    OT2.innerText = parseInt(NH.innerText);     
                                    NH.innerText=0;                                      
                                    //NHTOTAL=0; 
                                       TWH.innerText =OT2.innerText ;                  
                                 }
                                 
                                 if(converHrd.innerText=='OT1 To NH')
                                 {
                                   // NHTOTAL     = parseInt(NHTOTAL) + parseInt(OT1TOTAL);   
                                    NH.innerText = parseInt(NH.innerText) + parseInt(OT1.innerText);    
                                    OT1.innerText=0;
                                   //OT1TOTAL=0;
                                      TWH.innerText =NH.innerText; 
                                                            
                                 }   
                                 
                                 if(converHrd.innerText=='OT1 To OT2')
                                 {
                                    //OT2TOTAL        = parseInt(OT1TOTAL);   
                                    OT2.innerText   = parseInt(OT1.innerText) ;   
                                    OT1.innerText=0;
                                    //OT1TOTAL=0;   
                                    TWH.innerText =OT2.innerText ;                                                         
                                 }
                                 
                                  if(converHrd.innerText=='NH+OT1 To NH')
                                 {
                                      //NHTOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);      
                                      NH.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);   
                                      OT1.innerText=0;  
                                      //OT1TOTAL=0;
                                      TWH.innerText =NH.innerText;
                                                               
                                 }
                                 //alert(parseInt(NH.innerText));                                 
                                  if(converHrd.innerText=='NH+OT1 To OT1')//NH+OT1 To OT1
                                 {
                                     // OT1TOTAL      = parseInt(NHTOTAL) + parseInt(OT1TOTAL); 
                                      OT1.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);     
                                      
                                      NH.innerText=0;
                                      //OT1.innerText=0;
                                      //NHTOTAL=0;                                                                      
                                      TWH.innerText =OT1.innerText;
                                 }
                                 
                                  if(converHrd.innerText=='NH+OT1 To OT2')
                                 {
                                     //OT2TOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);      
                                     OT2.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);          
                                     NH.innerText=0;
                                     OT1.innerText=0;
                                     //NHTOTAL=0;
                                     //OT1TOTAL=0;
                                     TWH.innerText =OT2.innerText;
                                                                   
                                 }
                                 
                                 //alert(OT2.innerText);
                                 var NHH            = (NH.innerText/60);
                                 var NHM            = (NH.innerText%60);
                                 
                                 var OT1H           = (OT1.innerText/60);
                                 var OT1M           = (OT1.innerText%60);
                                 
                                 var OT2H           = (OT2.innerText/60);
                                 var OT2M           = (OT2.innerText%60);
                                 
                                 var TotalAH        = (TWH.innerText/60);
                                 var TotalAM        = (TWH.innerText%60);
                                 
                                   
                                 
//                              NHAT.getElementsByTagName("input")[0].value      =   NHH     +   '.' + NHM;
//                              OT1AT.getElementsByTagName("input")[0].value     =   OT1H    +   '.' + OT1M;
//                              OT2AT.getElementsByTagName("input")[0].value     =   OT2H    +   '.' + OT2H;
//                              TotalAT.getElementsByTagName("input")[0].value   =   TotalAH +   '.' + TotalAM;

                                 var objhr = new Object();
                                 var rnd = masterTableView.getCellByColumnUniqueName(row, "Rounding").innerText;
                                 
                                 //alert(NHH);
                                 //alert(NHM);
                                 
                                 objhr =rounding(NHH,NHM,rnd);                                                                                                                                                                    
                                 NHH = objhr.hr;
                                 NHM = objhr.minutes;                                  
                                 if(parseInt(NHM)<parseInt(10))
                                 {
                                    NHM="0"+NHM;
                                 }                               
                                 if(outTimeCell.getElementsByTagName("input")[0].value=='__:__' || outTimeCell.getElementsByTagName("input")[0].value=='')
                                 {
                                    NHA.innerText=='0 : 00';
                                 }
                                 else
                                 {
                                    //validrecord='false';
                                    //msg='OutTime Can not be blank';
                                    //errorNew.innerText=1;
                                    //outTimeCell.getElementsByTagName("input")[0].value=inTimeCell.getElementsByTagName("input")[0].value;
                                    NHA.innerText          = parseInt(NHH)        + ' : ' + NHM;        //Math.floor(NH.innerText/60);                                    
                                 } 
                                 
                                
                                 if(NHA.innerText=='0 : 00')
                                 {
                                    NHA.innerText='-';
                                 }
                                 objhr =rounding(OT1H,OT1M,rnd);
                                 OT1H = objhr.hr;
                                 OT1M = objhr.minutes;                                   
                                 if(parseInt(OT1M)<parseInt(10))
                                 {
                                    OT1M="0"+OT1M;
                                 } 
                                  
                                 if(outTimeCell.getElementsByTagName("input")[0].value=='__:__' || outTimeCell.getElementsByTagName("input")[0].value=='')
                                 {
                                    OT1A.innerText =='0 : 00';
                                 }
                                 else
                                 {
                                    OT1A.innerText         = parseInt(OT1H)        + ' : ' + OT1M;       //Math.floor(NH.innerText/60);
                                 }
                                 
                                  if(OT1A.innerText =='0 : 00')
                                 {
                                    OT1A.innerText ='-';
                                 }
                                 
                                 
                                 objhr =rounding(OT2H,OT2M,rnd);
                                 OT2H = objhr.hr;
                                 OT2M = objhr.minutes; 
                                 if(parseInt(OT2M)<parseInt(10))
                                 {
                                    OT2M="0"+OT2M;
                                 }
                                 
                                 if(outTimeCell.getElementsByTagName("input")[0].value=='__:__' || outTimeCell.getElementsByTagName("input")[0].value=='')
                                 {
                                    OT2A.innerText =='0 : 00';
                                 }
                                 else
                                 {
                                    OT2A.innerText         = parseInt(OT2H)        + ' : ' + OT2M;       //Math.floor(NH.innerText/60);
                                 }
                                 
                                 //alert(OT2A.innerText); 
                                 if( OT2A.innerText =='0 : 00')
                                 {
                                    OT2A.innerText ='-';
                                 }
                                 
                                 objhr =rounding(TotalAH,TotalAM,rnd);
                                 TotalAH = objhr.hr;
                                 TotalAM = objhr.minutes; 
                                 if(parseInt(TotalAM)<parseInt(10))
                                 {
                                    TotalAM="0"+TotalAM;
                                 }
                                 if(outTimeCell.getElementsByTagName("input")[0].value=='__:__' || outTimeCell.getElementsByTagName("input")[0].value=='')
                                 {
                                    TotalA.innerText =='0 : 00';
                                 }
                                 else
                                 {
                                    TotalA.innerText       = parseInt(TotalAH)     + ' : ' + TotalAM;    //Math.floor(NH.innerText/60);
                                 }
                                 
                                 if( TotalA.innerText =='0 : 00')
                                 {
                                     TotalA.innerText ='-';
                                 }
                                 
                                 NHTOTAL                =   parseInt(NHTOTAL)       +   parseInt(NH.innerText);
                                 OT1TOTAL               =   parseInt(OT1TOTAL)      +   parseInt(OT1.innerText);
                                 OT2TOTAL               =   parseInt(OT2TOTAL)      +   parseInt(OT2.innerText);
                                 TTOTAL                 =   parseInt(TTOTAL)        +   parseInt(TWH.innerText);
                                 
                                 NHAT.getElementsByTagName("input")[0].value        =   NHA.innerText ;
                                 OT1AT.getElementsByTagName("input")[0].value   =   OT1A.innerText ;
                                 OT2AT.getElementsByTagName("input")[0].value   =   OT2A.innerText ;
                                 TotalAT.getElementsByTagName("input")[0].value =   TotalA.innerText;
                          }
                          
//                              var masterTable1 = $find("<%= RadGrid2.ClientID%>").get_masterTableView();   
//                              var footer       = masterTable1.get_element().getElementsByTagName("TFOOT")[0];  
//                             
//                              var NHTOTAL1H =(NHTOTAL/60);
//                              var NHTOTAL1M =(NHTOTAL%60);
//                              
//                              var OT1TOTAL1H =(OT1TOTAL/60);
//                              var OT1TOTAL1M =(OT1TOTAL%60);
//                              
//                              var OT2TOTAL1H =(OT2TOTAL/60); 
//                              var OT2TOTAL1M =(OT2TOTAL%60); 
//                              
//                              var TTOTALH1=(TTOTAL/60);
//                              var TTOTALM1=(TTOTAL%60);
//                              
//                              //alert( parseInt(TTOTALH1)+ ' : ' + TTOTALM1);
//                                 
//                               objhr =rounding(NHTOTAL1H,NHTOTAL1M,rnd);
//                              NHTOTAL1H = objhr.hr;
//                              NHTOTAL1M = objhr.minutes;    
//                                 
//                              footer.rows[0].cells[25].innerHTML    =  parseInt(NHTOTAL1H)      + ' : ' +   NHTOTAL1M;
//                              
//                              objhr =rounding(OT1TOTAL1H,OT1TOTAL1M,rnd);
//                              OT1TOTAL1H = objhr.hr;
//                              OT1TOTAL1M = objhr.minutes;    
//                              
//                              
//                              footer.rows[0].cells[26].innerHTML    =   parseInt(OT1TOTAL1H)    + ' : ' +   OT1TOTAL1M;
//                              
//                              
//                              //alert(OT2TOTAL1H);
//                              //alert(OT2TOTAL1M);
//                              
//                              objhr =rounding(OT2TOTAL1H,OT2TOTAL1M,rnd);
//                              
//                              
//                              OT2TOTAL1H = objhr.hr;
//                              OT2TOTAL1M = objhr.minutes; 
//                              
//                              footer.rows[0].cells[27].innerHTML    =   parseInt(OT2TOTAL1H)    + ' : ' +   OT2TOTAL1M;
//                              objhr =rounding(TTOTALH1,TTOTALM1,rnd);
//                              TTOTALH1 = objhr.hr;
//                              TTOTALM1 = objhr.minutes; 
//                              
//                              footer.rows[0].cells[28].innerHTML    =   parseInt(TTOTALH1)      + ' : ' +   TTOTALM1;
                          
                              //alert(NHTOTAL);
                              //alert(OT1TOTAL);
                              //alert(OT2TOTAL);
                              //var text = footer.rows[0].cells[0].innerHTML;  
                              
                                var objbtnUpdate = document.getElementById('btnUpdate');                                
                                var objbtnApprove= document.getElementById('btnApprove');
                                var objbtnDelete = document.getElementById('btnDelete');
                                var objbtnReject= document.getElementById('btnUnlock');
                                //var objbtnSubApprove= document.getElementById('btnSubApprove');
                                
                                                               
                                if(flag1=='false')
                                {
                                    objbtnUpdate.disabled   =  true;                                    
                                    objbtnApprove.disabled  =  true;  
                                   // objbtnSubApprove.disabled  =  true;   
                                    //objbtnDelete.disabled  =  true;   
                                    //objbtnReject.disabled  =  true;                                 
                                }
                                else
                                {   
                                   objbtnUpdate.disabled        =   false;                                   
                                   objbtnApprove.disabled       =   false; 
                                  // objbtnSubApprove.disabled  =  false;                                     
                                }
                               
                                if(flag2=='false')
                                {
                                    //objbtnDelete.disabled =  false;
                                    objbtnReject.disabled =  false;   
                                }
                                else
                                {   
                                   //objbtnDelete.disabled =true;                                   
                                   objbtnReject.disabled =true;                                   
                                }
                               
                                //Check For Rights 
                                var lblRIghts = document.getElementById('lblName');                                
                                if(lblRIghts.innerHTML=="VT")
                                {
                                     objbtnUpdate.disabled   =  true;                                    
                                     objbtnApprove.disabled  =  true; 
                                     objbtnReject.disabled =true; 
                                     objbtnDelete.disabled =true;      
                                }
                                
                                 if(lblRIghts.innerHTML=="VTST")
                                {
                                     if(flag1!='false')
                                     {
                                        objbtnUpdate.disabled   =  false;
                                     }
                                     objbtnApprove.disabled  =  true; 
                                     objbtnReject.disabled =true; 
                                     objbtnDelete.disabled =true;      
                                }
                                
                                 if(lblRIghts.innerHTML=="VTSTAT")
                                {
                                    if(flag1!='false')
                                    {
                                        objbtnUpdate.disabled   =  false;
                                     }
                                     objbtnApprove.disabled  =  false; 
                                     objbtnReject.disabled =true; 
                                     objbtnDelete.disabled =true;      
                                }
                                
                                 if(lblRIghts.innerHTML=="SADMIN")
                                 {
                                    if(flag1!='false')
                                    {
                                        objbtnUpdate.disabled   =  false;
                                     }
                                     objbtnApprove.disabled  =  false; 
                                     objbtnReject.disabled =false; 
                                     objbtnDelete.disabled =false;   
                                 }
                         
                    }  //Function ends
                    
                    function rounding(hour,min,round)
                    {
                                var oDiff = new Object();
                                var factor =0;
                                
                                if(min==0 ||round==0 )
                                {
                                     oDiff.hr=hour;
                                     oDiff.minutes=min;
                                
                                }
                                else if(round==30)
                                {
                                    if(min==30)
                                    {
                                        oDiff.hr        =hour;
                                        oDiff.minutes   =min;
                                    }
                                    
                                    if(min<30)
                                    {
                                       oDiff.hr         =hour;
                                       oDiff.minutes    =0;
                                    }
                                    
                                    if(min>30)
                                    {
                                         oDiff.hr         =hour;
                                         oDiff.minutes    =30;
                                    }
                                    
                                }
                                else if(round==15)
                                {
                                
                                    //alert(min);
                                    //alert(hour);
                                    
                                    if(min==15)
                                    {
                                        oDiff.hr        =hour;
                                        oDiff.minutes   =min;
                                    }
                                    if(min==30)
                                    {
                                        oDiff.hr        =hour;
                                        oDiff.minutes   =min;
                                    }
                                    if(min==45)
                                    {
                                        oDiff.hr        =hour;
                                        oDiff.minutes   =min;
                                    }
                                    
                                    if(min>0 && min<15)
                                    {
                                       oDiff.hr         =hour;
                                       oDiff.minutes    =0;
                                    }
                                    
                                    if(min>15 && min<30)
                                    {
                                       oDiff.hr         =hour;
                                       oDiff.minutes    =15;
                                    }
                                    
                                    if(min>30 && min<45)
                                    {
                                       oDiff.hr         =hour;
                                       oDiff.minutes    =30;
                                    }
                                    
                                    if(min>45  && min<60)
                                    {
                                       oDiff.hr         =hour;
                                       oDiff.minutes    =45;
                                    }
                                    
                                }
                                else if(round==10)
                                {
                                            if(min==10)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==20)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==30)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==40)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==50)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min>0 && min<10)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =0;
                                            }
                                            
                                            if(min>10 && min<20)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =10;
                                            }
                                            
                                            if(min>20 && min<30)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =20;
                                            }
                                            
                                             if(min>30 && min<40)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =30;
                                            }
                                            
                                             if(min>40 && min<50)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =40;
                                            }
                                            
                                             if(min>50 && min<59)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =50;
                                            }
                                    
                                }
                                else if(round==5) 
                                {
                                            if(min==5)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==10)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==15)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==20)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==25)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            if(min==30)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min==35)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min==40)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min==45)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min==50)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min==55)
                                            {
                                                oDiff.hr        =hour;
                                                oDiff.minutes   =min;
                                            }
                                            
                                            if(min>0 && min<5)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =0;
                                            }
                                            
                                            if(min>5 && min<10)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =05;
                                            }
                                            
                                            if(min>10 && min<15)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =10;
                                            }
                                            
                                            if(min>15 && min<20)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =15;
                                            }
                                            
                                             if(min>20 && min<25)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =20;
                                            }
                                            
                                             if(min>25 && min<30)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =25;
                                            }
                                            
                                             if(min>30 && min<35)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =30;
                                            }
                                            
                                             if(min>35 && min<40)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =35;
                                            }
                                            
                                             if(min>40 && min<45)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =40;
                                            }
                                            
                                             if(min>45 && min<50)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =45;
                                            }
                                            
                                             if(min>50 && min<55)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =50;
                                            }
                                            
                                              if(min>55 && min<59)
                                            {
                                               oDiff.hr         =hour;
                                               oDiff.minutes    =55;
                                            }
                                    
                                }
                                else
                                {
                                     oDiff.hr=hour;
                                     oDiff.minutes=min;
                                }
                                
                               
                                return oDiff;
                    }
                    
                    
                    function timeHourDifference(endDate,startDate) 
                    { 
                        var difference = parseInt(((endDate.getTime()/1000.0) - (startDate.getTime()/1000.0))/60);
                        return difference; 
                    }    
                        
            
                   
                   function timeHourDifferenceCD(earlierDate,laterDate)
                    {
                           var nTotalDiff = laterDate.getTime() - earlierDate.getTime();
                           var oDiff = new Object();
                     
                           oDiff.days = Math.floor(nTotalDiff/1000/60/60/24);
                           nTotalDiff -= oDiff.days*1000*60*60*24;
                     
                            //alert(nTotalDiff);
                           oDiff.hours = Math.floor(nTotalDiff/1000.0/60.0/60.0);
                           nTotalDiff -= oDiff.hours*1000*60*60;
                     
                           oDiff.minutes = Math.floor(nTotalDiff/1000/60);
                           nTotalDiff -= oDiff.minutes*1000*60;
                     
                           oDiff.seconds = Math.floor(nTotalDiff/1000);
                     
                           //alert(oDiff.hours+'.' +  oDiff.minutes);                           
                           //alert(parseInt((oDiff.hours*60)+ oDiff.minutes ));
                           return parseInt((oDiff.hours*60)+ oDiff.minutes );                     
                    }

                   
                   function ShowDate(ED) 
                   { 
                            var now = new Date(); 
                            now=ED;
                            var then = now.getFullYear()+'-'+(now.getMonth()+1)+'-'+now.getDay(); 
                            then += ' '+now.getHours()+':'+now.getMinutes(); 
                            return then;
                        //alert(now+'\n'+then); 
                    } 

           </script>
                      
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


        <script type="text/javascript">
        
                                    function stopPropagation(e)
                                    {
                                        e.cancelBubble = true;
                                        
                                        if (e.stopPropagation)
                                        {
                                            e.stopPropagation();
                                        }
                                    }
        </script>

        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td style="width: 100%">
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" >
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Timesheet Project</b></font>
                            </td>
                        </tr>                       
                    </table>
                </td>
            </tr>            
        </table>
        
        <table  border="0"  cellpadding="0" cellspacing="0" width="100%">                
                  
                    <tr valign="bottom">
                        <td style="width:100%" valign="bottom"  >
                            <table width="100%"    >
                                <tr>
                                    <td style="margin-left: 1px;" >
                                        <table >
                                            <tr>
                                                     <td class="bodytxt" align="left"  valign="bottom" >
                                                        Project 
                                                    </td>
                                                    
                                                     <td class="bodytxt" align="left"  valign="bottom" >
                                                        Start Date 
                                                    </td>
                                                    
                                                     <td class="bodytxt" align="Left"  valign="bottom" style="vertical-align: bottom"  >
                                                        End Date 
                                                    </td>                        
                                                   
                                                     <td class="bodytxt" valign="bottom"  align="Left" style="width:5%" >
                                                         NightShift 
                                                     </td> 
                                                     <td>
                                                     
                                                     </td> 
                                            </tr>
                                            <tr>
                                                 <td   class="bodytxt" align="left"  valign="top"  style="width:0%;"   > 
                                                    <radG:RadComboBox   ID="RadComboProject" runat="server" EmptyMessage="Select a Employee" 
                                                      HighlightTemplatedItems="true" EnableLoadOnDemand="true"  Height="200px"  DropDownWidth="200px"    BorderWidth="0px" >
                                                    
                                                    </radG:RadComboBox>

                                                             <radG:RadComboBox ID="RadComboBoxEmpPrj" runat="server" Visible="false"  BorderWidth="0px" 
                                                AutoPostBack="true" EmptyMessage="Select a Employee" HighlightTemplatedItems="true"
                                                EnableLoadOnDemand="true" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested" DropDownWidth="275px"  Height="200px"
                                                OnSelectedIndexChanged="RadComboBoxEmpPrj_SelectedIndexChanged">
                                                <HeaderTemplate>
                                                    <table class="bodytxt" cellspacing="0" cellpadding="0" style="width: 260px">
                                                        <tr>
                                                            <td style="width: 80px;">
                                                                Project Code</td>
                                                             <td style="width: 180px;white-space:nowrap"  >
                                                                ProjectName</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table class="bodytxt" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 80px;">
                                                                <%# DataBinder.Eval(Container, "Attributes['ID']")%>
                                                            </td>                                                           
                                                            <td style="width: 180px;">
                                                                <%# DataBinder.Eval(Container, "Text")%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </radG:RadComboBox>
                                                 </td>                           
                               
                                                 <td class="bodytxt" align="left" valign="top" style="width:20%;vertical-align:top">
                                                        <radCln:RadDatePicker   Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart" runat="server">
                                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />                                                          
                                                        </radCln:RadDatePicker>
                                                </td>
                           
                                                 <td class="bodytxt"  align="Left" valign="top"  style="width:20%;vertical-align:top" colspan="" >
                                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd"  Enabled="false"
                                                            runat="server">
                                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                        </radCln:RadDatePicker>
                                                 </td >
                                                 
                                                 <td   class="bodytxt" valign="top" align="left" style="vertical-align:top"  >
                                                                    <asp:CheckBoxList  Visible="True" ID="chkrecords" runat="server" CssClass="bodytxt" ValidationGroup="val1" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"  CausesValidation="true" >
                                                                    <asp:ListItem   Selected="False" ></asp:ListItem>                                    
                                                                     </asp:CheckBoxList>
                                                 </td>    
                                                                            
                                                  <td align="left" class="bodytxt"  style="vertical-align:middle" >
                                                        <asp:ImageButton ID="imgbtnfetchEmpPrj" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />                                               
                                                  </td> 
                                       
                                            </tr>
                                            
                                            <tr>
                                            
                                            </tr>
                                        
                                        </table>
                                    </td>
                                    
                                    <td style="vertical-align: top">
                                        <table>
                                                    <tr>
                                                            
                                                            <td class="bodytxt"  valign="bottom" align="Left">
                                                                 In Time                                            
                                                             </td>                         
                                                            <td class="bodytxt"  valign="bottom" align="Left" >
                                                                Out Time 
                                                             </td>
                                                             
                                                             <td>
                                                             
                                                             </td>
                                                             
                                                             <td>
                                                             
                                                             </td>
                                                             
                                                             <td>
                                                             
                                                             </td>
                                                        
                                                        
                                                        </tr>
                                                        
                                                        
                                                        
                                                    <tr>
                                                             <td class="bodytxt" align="left"  valign="top" style="width:15%;" >
                                                                     <asp:TextBox Visible="True" Text='' ID="DeftxtInTime" runat="server"  Width="70%"
                                                                   ValidationGroup="vldSum" /></td>                                            
                                
                                                             <td class="bodytxt"  align="left" valign="top"  style="width:15%;" >
                                                                          <asp:TextBox Visible="True" Text='' ID="DeftxtOutTime" runat="server" Width="70%"
                                                                              ValidationGroup="vldSum" /></td> 
                                                                              
                                                             <td align="left" valign="top" style="vertical-align:top" >
                                                                        <asp:Button ID="btnCopy" Visible="true" runat="server"  Text="Key In/Out Time" OnClientClick="return Copy()" />
                                                             </td>                                                                             
                                
                                                             <td  style="width:0%;" valign="top">
                                                                        <ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderIn" runat="server"     TargetControlID="DeftxtInTime"
                                                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US"      />
                                                                            <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorIn" runat="server" ControlExtender="DefMaskedEditExtenderIn"
                                                                                ControlToValidate="DeftxtInTime" IsValidEmpty="False" InvalidValueMessage="*"
                                                                                ValidationGroup="vldSum"    Display="Dynamic" />
                                                                    </td>                                
                               
                                                             <td  valign="top" >
                                                                     <ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderOut" runat="server" TargetControlID="DeftxtOutTime"
                                                                       Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                         MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                                             </td>
                                                             <td  valign="top" >
                                                                    <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorOut" runat="server" ControlExtender="DefMaskedEditExtenderOut"
                                                                            ControlToValidate="DeftxtOutTime" IsValidEmpty="False" InvalidValueMessage="*"
                                                                            ValidationGroup="vldSum" Display="Dynamic" /> 
                                                             </td>
                                                        
                                                        </tr>                                                
                                                
                                                </table>
                                    </td> 
                                </tr>
                            </table>
                        </td>              
                    </tr> 
                    <tr>
                        
                        <td >
                            <table width="100%" style="border-style:groove;border-color: blue;border-collapse: separate;" border="1">
                            
                            </table>    
                        </td>
                    </tr>                     
                    <tr >
                        <td  style="width:100%;" >
                            <table align="right" style="border-style: outset;border-color: blue;border-collapse: separate; border-top-width:0px"  border="1">
                                <tr>
                                    <td  >
                                        <asp:Button ID="btnUpdate" runat="server" Text="Submit" OnClick="btnUpdate_Click" />
                                    </td> 
                                    <td>
                                        <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Visible="true" 
                                            Text="Approve" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubApprove" runat="server"   Visible="false"  Enabled="false" 
                                            Text="Submit/Approve" />
                                    </td>
                                    <td  >
                                         <asp:Button ID="btnUnlock" runat="server"  Visible="true" Text="Unlock" />
                                    </td>
                                    <td  >
                                         <asp:Button ID="btnDelete" runat="server" Visible="true" OnClick="btnDelete_Click" Text="Delete" />
                                    </td> 
                                    
                                </tr>
                            </table>
                        </td>                             
                                      
                    </tr>                                     
                   <tr id="tr1" runat="server">                 
                    
                   </tr>    
        </table>
        <br />
        <table id="tbl1" runat="server" border="0" cellpadding="1" cellspacing="0">
             <tr >
                <td align="center">
                    <tt class="bodytxt">
                        <asp:Label ID="lblMsg"  runat="server"  ForeColor="Maroon" Width="80%"></asp:Label></tt>
                </td>               
            </tr>            
            <tr>
                <td align="left" >
                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                        <script type="text/javascript">
                            function RowDblClick(sender, eventArgs)
                            {
                                //sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                //alert('hi1');
                                  //eventArgs.set_cancel(true); 
                                  
                                              
                                    //                                    var grid = $find("<%=RadGrid2.ClientID %>");	                    
                                    //                                    var masterTableView = grid.get_masterTableView();
                                    //                                    var selectedRows = masterTableView.get_selectedItems();                        
                                    //                                    //Check Roster Type
                                    //                                    var rosterType;
                                    //                                    var msg='';
                                    //                                    var rowno='';
                                    //                                    
                                    //                                    //alert('hi');
                                    //                                    for (var i = 0; i < selectedRows.length; i++) 
                                    //                                    { 
                                    //                                        var row                  =   selectedRows[i];                                
                                    //                                        var cell                 =   masterTableView.getCellByColumnUniqueName(row, "GridClientSelectColumn"); 
                                    //                                        alert(cell.innerHTML);
                                    //                                    }
                                    
                            }
                        </script>

                    </radG:RadCodeBlock>
                    
                     <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" >                     
                             <ContentTemplate>
                                        
                    
                    <radG:RadGrid ID="RadGrid2" runat="server" 
                        OnItemDataBound="RadGrid2_ItemDataBound"   
                        Width="90%" 
                        AllowFilteringByColumn="false" 
                        AllowSorting="true" 
                        Skin="Outlook"  
                        EnableAjaxSkinRendering="true" 
                        MasterTableView-AllowAutomaticUpdates="true"
                        MasterTableView-AutoGenerateColumns="false" 
                        MasterTableView-AllowAutomaticInserts="False"
                        MasterTableView-AllowMultiColumnSorting="False" 
                        GroupHeaderItemStyle-HorizontalAlign="left"
                        ClientSettings-EnableRowHoverStyle="false" 
                        ClientSettings-AllowColumnsReorder="false"
                        ClientSettings-ReorderColumnsOnClient="false" 
                        ClientSettings-AllowDragToGroup="False" 
                        ShowFooter="true"    
                        ShowStatusBar="true"                    
                        AllowMultiRowSelection="true" 
                        
                        PageSize="50"  
                        AllowPaging="true"   >
                        
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <SelectedItemStyle CssClass="SelectedRow" /> 
                                                
                        <MasterTableView  >
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px"  />                            
                            
                            <Columns>
                                  <radG:GridBoundColumn DataField="RosterType"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="RosterType"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="RosterType"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="RosterType"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="OutTime"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="OutTime"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="InTime"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="InTime"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="EarlyInBy"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyInBy"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="LateInBy"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="LateInBy"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="EarlyInBy"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyInBy"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="EarlyOutBy"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyOutBy"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="LateOutBy"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="LateOutBy"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>
                                    <%--17--%>
                                <radG:GridClientSelectColumn  HeaderStyle-HorizontalAlign="Center" UniqueName="GridClientSelectColumn"    >
                                    <ItemStyle  HorizontalAlign="center" Width="2%"  />                                    
                                </radG:GridClientSelectColumn>
                            
                            <%--2--%>
                                <radG:GridTemplateColumn HeaderText="Add"  Visible="false"  HeaderButtonType="PushButton"     HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                      <ItemTemplate >  
                                          <asp:ImageButton id="btnAdd" Visible="false" Enabled="false" runat="server"  CommandName="AddNew" ImageUrl="~/Frames/Images/STATUSBAR/Add-ss.png" ToolTip="Add New Record" AlternateText="Add" />
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
                                 <radG:GridTemplateColumn  HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" UniqueName="Employee" ItemStyle-Wrap="false" >   
                                  <ItemTemplate >                                        
                                      <asp:Label    ID="lblEmp" Width="100%"  runat="server" CssClass="bodytxt" ></asp:Label>
                                      
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Left" Width="12%" />
                                </radG:GridTemplateColumn>
                               <%--7--%>     
                                 <radG:GridTemplateColumn  Display="false"  HeaderText="Project Name"  HeaderStyle-HorizontalAlign="Center"  UniqueName="Project" >
                                  <ItemTemplate >  
                                      <asp:DropDownList ID="drpProject" Width="100%" runat="server" CssClass="bodytxt"  Enabled="false" style="border-right: #cccccc 0px solid; table-layout: fixed;border-top: #cccccc 0px solid; 
                                          border-left: #cccccc 0px solid;border-bottom: #cccccc 0px solid;">
                                      </asp:DropDownList>  
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center"  Width="15%" />
                                </radG:GridTemplateColumn>                                 
                               <%--8--%>     
                                 <radG:GridTemplateColumn HeaderText="Start Date"    HeaderStyle-HorizontalAlign="Center"      UniqueName="SD" >
                                  <ItemTemplate >  
                                      <asp:DropDownList ID="drpSD" runat="server"  Enabled=false CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >   
                                      </asp:DropDownList>                                        
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </radG:GridTemplateColumn>
                                
                                <%--09--%>    
                                 <radG:GridTemplateColumn Display="false" HeaderText="End Date" HeaderStyle-HorizontalAlign="Center"  UniqueName="ED"  >
                                  <ItemTemplate >  
                                      <asp:DropDownList ID="drpED" runat="server" Enabled=false CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >   
                                      </asp:DropDownList>  
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center"    Width="5%" />
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
                                <radG:GridBoundColumn DataField="Err"  Visible="false"  HeaderStyle-ForeColor="black"  HeaderText="Err" HeaderStyle-HorizontalAlign="Center" UniqueName="Err" 
                                    Display="True">
                                    <ItemStyle HorizontalAlign="Center" Width="0%" />
                                </radG:GridBoundColumn>
                                
                                <radG:GridTemplateColumn HeaderText=""   UniqueName="ErrImg" Display="true" HeaderStyle-HorizontalAlign="Center"   >   
                                        <ItemTemplate>   
                                                    <asp:Image  style="cursor:hand" ID="Image2" runat="server"  BorderStyle="Inset"  ImageUrl="~/Frames/Images/STATUSBAR/alert4.png" />
                                                    
                                                    <telerik:RadToolTip EnableAjaxSkinRendering="true"  ShowCallout="true"  Visible="false"  
                                                            Overlay="true" ID="RadToolTip1"  Animation="Slide"   
                                                            runat="server" TargetControlID="Image1" Width="500px" Height="200px" 
                                                            MouseTrailing="true" RelativeTo="Element" Position="MiddleRight" 
                                                            EnableShadow="true" ShowDelay="2"   >
                                                            Valid Data1
                                                    </telerik:RadToolTip>
                                        </ItemTemplate>  
                                          <ItemStyle  HorizontalAlign="center" Width="4%" /> 
                                </radG:GridTemplateColumn >
                                
                                 <%--14--%>    
                               <radG:GridBoundColumn DataField="Roster_Day" UniqueName="Roster_Day" 
                                    HeaderText="Day" Display="True" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                     <ItemStyle HorizontalAlign="Center" Width="2%" />
                                </radG:GridBoundColumn>
                                
                                <%--15--%>    
                                 <radG:GridTemplateColumn DataField="InShortTime" UniqueName="InShortTime" HeaderText="In Time" HeaderStyle-HorizontalAlign="Center"
                                    AllowFiltering="false" Groupable="false" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.InShortTime")%>' ID="txtInTime" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid"
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
                                <radG:GridTemplateColumn DataField="OutShortTime" UniqueName="OutShortTime" HeaderText="Out Time" HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                    AllowFiltering="false" Groupable="false"  >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OutShortTime")%>' ID="txtOutTime" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid"
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
                                
                                 <radG:GridBoundColumn DataField="NHA"      DataType="System.Decimal" HeaderStyle-HorizontalAlign="Center" UniqueName="NHA" HeaderText="NH"   FooterStyle-HorizontalAlign="Center" 
                                        FooterStyle-Font-Bold="true" ItemStyle-Wrap="false" FooterStyle-Wrap="false" 
                                        Display="True">
                                     <ItemStyle Width="10%" HorizontalAlign="center" Font-Size=Small   BackColor=LightSteelBlue />
                                </radG:GridBoundColumn>                               
                                <radG:GridBoundColumn DataField="OT1A"    DataType="System.Decimal"  HeaderStyle-HorizontalAlign="Center" UniqueName="OT1A" HeaderText="OT1" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                    Display="True">
                                     <ItemStyle Width="8%" HorizontalAlign="center" Font-Size=Small BackColor=LightSteelBlue />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT2A"    DataType="System.Decimal"  HeaderStyle-HorizontalAlign="Center" UniqueName="OT2A" HeaderText="OT2" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                    Display="True">
                                     <ItemStyle Width="8%" HorizontalAlign="center" Font-Size=Small BackColor=LightSteelBlue />
                                 </radG:GridBoundColumn>
                                 
                                <radG:GridBoundColumn DataField="TotalA"    DataType="System.Decimal"  HeaderStyle-HorizontalAlign="Center" UniqueName="TotalA" HeaderText="Total" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                    Display="True">
                                     <ItemStyle Width="8%" HorizontalAlign="center" Font-Size=Small BackColor=LightSteelBlue />
                                 </radG:GridBoundColumn>
                                 
                               <radG:GridTemplateColumn DataField="NHAT"   DataType="System.string" HeaderStyle-HorizontalAlign="Center" UniqueName="NHAT" HeaderText="NHAT" 
                                    Display="false">
                                         <ItemTemplate>
                                            <div>
                                                <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NHAT")%>' ID="NHAT"
                                                    runat="server" Width="38px" />
                                            </div>                                            
                                       </ItemTemplate> 
                                    
                                     <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn> 
                                                              
                                <radG:GridTemplateColumn DataField="OT1AT"    DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="OT1AT" HeaderText="OT1T"
                                    Display="false">
                                    
                                       <ItemTemplate>
                                            <div>
                                                <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OT1AT")%>' ID="OT1AT"
                                                    runat="server" Width="38px" />
                                            </div>                                            
                                       </ItemTemplate> 
                                        
                                    
                                    
                                     <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                
                                <radG:GridTemplateColumn DataField="OT2AT"   DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="OT2AT" HeaderText="OT2T"
                                    Display="false">
                                    
                                           <ItemTemplate>
                                            <div>
                                                <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OT2AT")%>' ID="OT2AT"
                                                    runat="server" Width="38px" />
                                            </div>                                            
                                       </ItemTemplate> 
                                    
                                    
                                    
                                     <ItemStyle Width="8%" HorizontalAlign="center" />
                                 </radG:GridTemplateColumn>
                                 
                                <radG:GridTemplateColumn DataField="TotalAT"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="TotalAT" HeaderText="TotalT"
                                    Display="false">
                                    
                                           <ItemTemplate>
                                            <div>
                                                <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.TotalAT")%>' ID="TotalAT"
                                                    runat="server" Width="38px" />
                                            </div>                                            
                                       </ItemTemplate> 
                                    
                                     <ItemStyle Width="8%" HorizontalAlign="center" />
                                 </radG:GridTemplateColumn>
                                 
                                
                                <%--18--%>
                                <radG:GridBoundColumn DataField="SDate"   DataType="System.DateTime"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="SDate" HeaderText="SDate"
                                    Display="False">
                                </radG:GridBoundColumn>
                                 <%--19--%>    
                                <radG:GridBoundColumn DataField="EDate"  DataType="System.DateTime"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EDate" HeaderText="EDate"
                                    Display="False">
                                </radG:GridBoundColumn>
                                <%--20--%>  
                                 <radG:GridBoundColumn DataField="Roster_id" DataType="System.Int64"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="Roster_id"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="EarlyInBy" DataType="System.Int64"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyInBy"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="LateInBy" DataType="System.Int64"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="LateInBy"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="EarlyOutBy" DataType="System.Int64"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyOutBy"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="ErrNew" DataType="System.Int64"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="ErrNew"
                                    Display="false">
                                </radG:GridBoundColumn>
                                
                                 <radG:GridBoundColumn DataField="NewR"    HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="NewR"
                                    Display="false">
                                </radG:GridBoundColumn>
                                  <%--NHWHMINUTES DEDUCTED BREAK TIME --%> 
                                <radG:GridBoundColumn DataField="NHWHM"   HeaderStyle-ForeColor="black"  DataType="System.Int32" HeaderStyle-HorizontalAlign="Center" UniqueName="NHWHM" 
                                    Display="false">
                                </radG:GridBoundColumn>
                                <%--BREAK TIME --%> 
                                <radG:GridBoundColumn DataField="BKOT1"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="BKOT1" 
                                    Display="false">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="NH"   HeaderStyle-ForeColor="black"  DataType="System.Int32" HeaderStyle-HorizontalAlign="Center" UniqueName="NH" HeaderText="NH" 
                                    Display="false">
                                    <ItemStyle Width="12%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <%--BREAK TIME --%> 
                                <radG:GridBoundColumn DataField="OT1"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="OT1" HeaderText="OT1"
                                    Display="false">
                                    <ItemStyle Width="12%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT2"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="OT2" HeaderText="OT2"
                                    Display="false">
                                    <ItemStyle Width="12%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TWH"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="TWH" HeaderText="Total"
                                    Display="false">
                                     <ItemStyle Width="12%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="WHACT"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="WHACT" HeaderText="WHACT"
                                    Display="false">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="BTOTHR"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="BTOTHR" HeaderText="BTOTHR"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="BTNHHT"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="BTNHHT" HeaderText="BTNHHT"
                                    Display="False">
                                </radG:GridBoundColumn>
                                                                
                                <radG:GridBoundColumn DataField="BreakTimeNH"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="BreakTimeNH" HeaderText="BreakTimeNH"
                                    Display="false">
                                </radG:GridBoundColumn>
                                
                               <%-- <radG:GridBoundColumn DataField="NHWHREM"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="NHWHREM" HeaderText="NHWHREM"
                                    Display="True">
                                </radG:GridBoundColumn>
                                --%>
                                 <radG:GridTemplateColumn    Display="false"    DataField="NHWHREM" UniqueName="NHWHREM" HeaderText="NHWHREM" HeaderStyle-HorizontalAlign="Center"
                                    AllowFiltering="false" Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true" >
                                        <ItemTemplate>
                                            <div>
                                                <asp:TextBox  Text='<%# DataBinder.Eval(Container,"DataItem.NHWHREM")%>' ID="txtNHWHREM"
                                                    runat="server" Width="38px" />
                                            </div>                                            
                                       </ItemTemplate>                                                                                
                                </radG:GridTemplateColumn>
                                   <radG:GridBoundColumn DataField="EmailSup"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="EmailSup" HeaderText="EmailSup"
                                    Display="False">
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="ID"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="ID" HeaderText="ID"
                                    Display="False">
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="TrHrs"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="TrHrs" HeaderText="TrHrs"
                                    Display="false">
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn DataField="TrHrs"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="TrHrs" HeaderText="TrHrs"
                                    Display="false">
                                </radG:GridBoundColumn>
                                  <radG:GridBoundColumn DataField="Rounding"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="Rounding" HeaderText="Rounding"
                                    Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="BreakTimeAfter"   HeaderStyle-ForeColor="black" DataType="System.string"  HeaderStyle-HorizontalAlign="Center" UniqueName="BreakTimeAfter" HeaderText="BreakTimeAfter"
                                    Display="false">
                                </radG:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        
                            <ClientSettings>
                                <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false" AllowColumnResize="false">
                                    </Resizing>
                                    <ClientEvents OnGridCreated="GridCreated" OnRowClick="RowClick" OnRowSelected="RowDblClick"   ></ClientEvents>
                                    <Selecting AllowRowSelect="true" /> 
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
             <tr>
                <td  width="0%" height="0%">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
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
