<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RetailRoaster.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.RetailRoaster" %>

<%@ Register TagPrefix="uc1" TagName="BottomCtrl" Src="~/Frames/Bottom.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Weekly Roaster</title>
   <style type="text/css">
    @media print {
   .page
    {
     -webkit-transform: rotate(-90deg); -moz-transform:rotate(-90deg);
     filter:progid:DXImageTransform.Microsoft.BasicImage(rotation=3);
    }
    table
    {
    border-collapse:collapse;
  border: 1px solid black;
  width:100%;
  text-align:right;
  }
     body {
            font-family: arial;
            font-size: 8px ;
            font-colour:black
            }
 
        th,
        td {
            padding: 0px 0px 0px 0px ;
            text-align:left ;
            }
 
        th {
            border-bottom: 2px solid #333333 ;
            }
 
        td {
            border-bottom: 1px solid #999999 ;
            }
 
        tfoot td {
            border-bottom-width: 0px ;
            border-top: 2px solid #333333 ;
            padding-top: 20px ;
            }
  
}
    </style>
    
    
   <%-- <style type="text/css">
    @media print
{
	/* hide every element within the body */
	body * { display: none !important; }
	/* add a friendy reminder not to waste paper after the body */
	body:after { content: "Don't waste paper!"; }
}
        </style>--%>
        
        <style type="text/css">
      .kumar
{
  border-collapse:collapse;
  border: 1px solid black;
  width:100%;
  text-align:right;
}
        <style>
   
        
    <style type="text/css">
        /*

Darko Bunic
http://www.redips.net/
Nov, 2009.

*/body
        {
            font-family: arial;
            margin: 0px; /* for IE6 / IE7 */
        }
        /* add bottom margin between tables */#table1, #table2
        {
            margin-bottom: 20px;
        }
        /* drag container */#drag
        {
            margin: auto;
            width: 100%;
        }
        /* drag objects (DIV inside table cells) */.drag
        {
            cursor: move;
            margin: auto;
            z-index: 10;
            background-color: white;
            text-align: center;
            font-size: 10pt; /* needed for cloned object */
            opacity: 0.7;
            filter: alpha(opacity=70); /* without width, IE6/7 will not apply filter/opacity to the element ?! */
            width: 87px;
        }
        /* drag objects border for the first table */.t1
        {
            border: 2px solid #499B33;
        }
        /* drag object border for the second table */.t2
        {
            border: 2px solid #2D4B7A;
        }
        /* cloned objects - third table */.t3
        {
            /*border: 2px solid #BF6A30;*/
            border: 1px solid #2452B0;
            width:150px;
            padding-left: 2px;
            text-align: left;
        }
        /* allow / deny access to cells marked with 'mark' class name */.mark
        {
            color: white;
            background-color: #9B9EA2;
        }
        /* trash cell */.trash
        {
            color: white;
            background-color: #2D4B7A;
        }
        /* tables */div#drag table
        {
            background-color: #e0e0e0;
            border-collapse: collapse;
        }
        /* needed for IE6 because cursor "move" shown on radio button and checckbox ?! */div#drag input
        {
            cursor: auto;
        }
        /* table cells */div#drag td
        {
            height: 32px;
            border: 1px solid white;
            text-align: center;
            font-size: 10pt;
            padding: 2px;
        }
        /* "Click" button */.button
        {
            background-color: #6A93D4;
            color: white;
            border-width: 1px;
            width: 40px;
            padding: 0px;
        }
        /* toggle checkboxes at the bottom */.checkbox
        {
            margin-left: 13px;
            margin-right: 14px;
            width: 13px; /* needed for IE ?! */
        }
        /* button message */.message_line
        {
            padding-left: 10px;
            margin-bottom: 3px;
            font-size: 10pt;
            color: #888;
        }
        
        
        
        
        
        
/* drag container */
#main_container{
	margin: auto;
	width: 900px;
	
}
		/* container for the left table */
		#main_container #left{
			width: 150px;
			float: left;
			margin-right: 20px;
			height:30px;
			overflow:scroll;
		}
		/* container for the main table and message line below */
		#main_container #right{
			width: 75%;
			float:right;
			padding-left: auto;
			padding-right: 0px;
			/* align div to the right */
			margin-left: auto;
		}
    </style>

    <script src="../Frames/Script/jquery-1.3.2.min.js" type="text/javascript"></script>
     <script src="../Frames/Script/jspdf.min.js" type="text/javascript"></script>
     <script src="../Frames/Script/canvas-1.2.min.js" type="text/javascript"></script>


    <script src="../Frames/Script/redips-drag-min.js" type="text/javascript"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/1.3.1/js/toastr.min.js"
        type="text/javascript"></script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/1.3.1/css/toastr.min.css"
        type="text/css" />
    

    <script type="text/javascript">
      var statuscode; 
      var status_messages;
      var isserch;
        window.onload = function () 
        {
        
       //toastr.info('droped!!!');
         
         
            // reference to the REDIPS.drag class
            var rd = REDIPS.drag;
            // initialization
            rd.init();
            
            // dragged elements can be placed to the empty cells only
				//rd.drop_option = 'multiple';
             
            	  
//                rd.myhandler_droppedBefore = function () {
//                alert('droppedBefore');
//                }
               
               // same employee two time override one
               //rd.drag.multipleDrop = 'top';


                // Handle dropped event
                rd.myhandler_dropped = function () 
                {
              
                   
                 	//get source
            	    var sourceTdId= REDIPS.drag.source_cell 
            	    //alert(sourceTdId.id);
            	    if(sourceTdId.id=="")
            	    {
                     sourceTdId.id="FromEmpTable"
                     //alert(sourceTdId.id);
                    }
				
				    //get target	
				    var obj = rd.obj, 		// current element
			        tac = rd.target_cell; // target cell
				    var Emp= obj.id;
                     //alert('Emp='+Emp);
                     //var targer= obj.id + ',' + tac.id;
                     var target= tac.id;
                     //alert(target)
                    
                 
                
                 statuscode='';
                 var id,SourceTable;
                 if(sourceTdId.id=="FromEmpTable")// drag from employee table
                 {
                    id=tac.id;
                    SourceTable="FromEmpTable"
                 }
                 else
                 {
                    id=sourceTdId.id;
                    SourceTable="FromRoasterTable"
                 }
                
                //validation 1
                 //validationSameEmpCannotBeDifferentPatternOnSameDay(id,Emp,SourceTable)
                 validationSameEmpCannotBeDifferentPatternOnSameDay(id,Emp,target)
                   if(statuscode=="100" || statuscode=="")
                   {
                      
                      alert("Already assigned in "+status_messages);
                      //window.location.reload();
                      __doPostBack('form1', '');
                  
                    }
                    else if(statuscode=="102" ) 
                    {
                        alert("Already assigned in "+status_messages);
                         //window.location.reload();
                        __doPostBack('form1', '');
                    }
                   else if(statuscode=="103" ) 
                    {
                        alert("Emp in leave!");
                         //window.location.reload();
                        __doPostBack('form1', '');
                    }
                  else if(statuscode=="104" ) 
                    {
                        alert("Terminated!");
                         //window.location.reload();
                        __doPostBack('form1', '');
                    }
//                      else if(statuscode=="105" ) 
//                    {
//                        alert("No  of days for the week exceeded");
//                       __doPostBack('form1', '');
//                    }
////                      else if(statuscode=="106" ) 
////                    {
////                        alert("Working Days in a Only 5.5 days");
////                       
////                    }
                   else
                   {
                   
                     /*******validate birthday *************/
                     if(Birthday(sourceTdId.id,Emp,target)=="YES")
                     {
                     var answer = confirm ("Birthday!!..Still want to assign?")
                     }
                   
                     if (answer || Birthday(sourceTdId.id,Emp,target)=="NO" )
                      {
                     /**********End validate birthday ****************************
                   
                     /******* while drop- update in db ***********/ 
                     
                       var Checkboxval=0;
                       
                      
                      if(statuscode=="105")
                      {
                        if ($('#cbx15Times').is(':visible'))
                        {
                        
                        
                        
                        if ($('#cbx15Times').is(":checked"))
                        {
                          Checkboxval=8;
                        }
                       else
                        {
                        Checkboxval=0;
                        }
                        
                        
                        }
                        else
                        {
                          Checkboxval=8;
                        }
                        
                      }
                         
                           
                                            
                     var _remark= document.getElementById("remark").value;
                     
                      $.ajax({
                          type: "POST",
                          url: "RetailRoaster.aspx/UpdateDB",
                              data: "{'Source':'"+sourceTdId.id+"','Emp':'"+Emp+"','target':'"+target+"','Checkboxval':'"+Checkboxval+"','Remark':'"+_remark+"' }",
                          contentType: "application/json; charset=utf-8",
                          dataType: "json",
                          success: function(msg) {
                            alert(msg);
                            
                            if($('#search').val().length == 0)
                            {
                          __doPostBack('form1', '');
                            }
                          },
                          error:function(ts) {
                            alert(ts.responseTex);
                          },
                           failure: function(msg) {
                            alert(msg);
                          }
                        });
                      
                      
                     //  __doPostBack('form1', '');
                    //  window.location.reload();
                    /******* End while drop- update in db ***********/
                    
                    }
                    
                     else// if birthday and if they click "cancel" button
                      {
                        __doPostBack('form1', '');
                      }
                    
                  }
                 
                 
                
                 
                 }
                 
                 
                /************** While delete **************/ 
                rd.myhandler_deleted    = function () 
            	{
            	    //get source
            	    var sourceTdId= REDIPS.drag.source_cell 
                    //alert(sourceTdId.id);
				
				    //get target	
				    var obj = rd.obj, 		// current element
			        tac = rd.target_cell; // target cell
				    var Emp= obj.id;
				    
				    
				    //updte database
				      $.ajax({
                      type: "POST",
                      url: "RetailRoaster.aspx/DeleteInDB",
                        data: "{'Source':'"+sourceTdId.id+"','Emp':'"+Emp+"' }",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function(msg) {
                        alert(msg);
                      },
                      error:function(msg) {
                        alert(msg.responseTex);
                      },
                       failure: function(msg) {
                        alert(msg.responseTex);
                      }
                    });
				    //
					
            	}
            	
                /************** before delete **************/ 
                 
                                                
            };










        //validation to check whether same employee cannot in multiple pattern
        //100- already assign
        //101- not assigned, ready to use
      
        function validationSameEmpCannotBeDifferentPatternOnSameDay(sourceTdId,Emp,SourceTable)
        {
            status_messages="";
        
              $.ajax({
                      type: "POST",
                      url: "RetailRoaster.aspx/validateWhetherAssignAlready",
                      data: "{'Source':'"+sourceTdId+"','Emp':'"+Emp+"','SourceTable':'"+SourceTable+"' }",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      async: false,
                      success: function(msg) {
                        var res = msg.split("%");
                   status_messages=res[1];
                      statuscode=res[0];
                    
                      
                      },
                      error:function(msg) {
                      alert('Error:'+msg.responseTex);
                
                      },
                       failure: function(msg) {
                        alert('Fail:'+msg.responseTex);
                  
                      }
                    });
        
                    
                    
        }

      function Birthday(sourceTdId,Emp,target)
      {
        var status='NO%no';
        $.ajax({
                      type: "POST",
                      url: "RetailRoaster.aspx/ValidateBirthday",
                        data: "{'Source':'"+sourceTdId+"','Emp':'"+Emp+"','target':'"+target+"' }",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      async: false,
                      success: function(msg) {
                         var res1 = msg.split("%");
                   
                      status=res1[0];
                      },
                      error:function(msg) {
                      alert('Error:'+msg.responseTex);
                
                      },
                       failure: function(msg) {
                        alert('Fail:'+msg.responseTex);
                  
                      }
                    });
      
        //return 'YES';
        return status;
      }

    </script>

    <%--Emp table search--%>

    <script type="text/javascript">
$(document).ready(function()
{
	$('#search').keyup(function()
	{
	    
		searchTable($(this).val());
	});
});

function searchTable(inputVal)
{

	var table = $('#table1');
	table.find('tr').each(function(index, row)
	{
		var allCells = $(row).find('td');
		if(allCells.length > 0)
		{
			var found = false;
			allCells.each(function(index, td)
			{
				var regExp = new RegExp(inputVal, 'i');
				if(regExp.test($(td).text()))
				{
					found = true;
					return false;
				}
			});
			if(found == true)$(row).show();else $(row).hide();
		}
	});
	
	
}
    </script>

    <script type="text/javascript">
    

$(function () {
   
//     var doc = new jsPDF('p', 'pt', 'letter');
//    var specialElementHandlers = {
//        '#editor': function (element, renderer) {
//            return true;
//        }
//    };
       
       
       
       
       
       
       
    $('#cmd').click(function () {
            
//           var table=$('#TableDiv').removeClass("mark").addClass("kumar").html();
//        
//           alert(table);
//     
//        doc.fromHTML(table, 15, 15, {
//            'width': 522,
//                'elementHandlers': specialElementHandlers
//        });
//        doc.save('roster_print.pdf');

       // var tr=document.getElementById("btmCtrl_CompanyName").value;
        
       // alert(tr);
        
        
    // $('#TableDiv').append($('#btmCtrl_CompanyName').val());
    $('#TableDiv table').removeClass().removeAttr("style").attr("border","1");
    $('#TableDiv tbody').removeClass().removeAttr("style");
    $('#TableDiv tr').removeClass().removeAttr("style");
    $('#TableDiv td').removeClass().removeAttr("style");
     $('#TableDiv div').removeAttr("style");
 
     var divToPrint=document.getElementById("TableDiv");
      var remark= document.getElementById("remark");
    
  $('#tblPage tr:last').remove();
  
//     divToPrint.setAttribute('class','kumar');
//  
//       divToPrint.setAttribute('style','');
   newWin= window.open("");
  //alert(divToPrint.outerHTML);
   newWin.document.write(divToPrint.outerHTML);    
   newWin.document.write(remark.outerHTML);
   newWin.print();
   newWin.document.close();
   newWin.close();
        
    });
});

    
    
    
    
    
    

    function PrintElem(printpage)
    {
   var el=  document.getElementsByTagName("div")[0]
//// el.style.cssText = null
       el.removeAttribute("class");
     document.getElementById("tblPage").deleteRow(-1);
//        var headstr = "<html><head>   </head><body>";
//        var footstr = "</body>";
    var newstr = document.all.item(printpage).innerHTML;
//       var oldstr = document.body.innerHTML;
//      var doc_print= headstr+newstr+footstr;
    
 
    
    
   //     doc.fromHTML(newstr, 15, 15, {
//            'width': 170,
//                'elementHandlers': specialElementHandlers
//        });
//        doc.save('sample-file.pdf');
    
  alert(newstr);
    
//        window.print();
//        document.body.innerHTML = oldstr;
//        return false;

    var doc = new jsPDF();
    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

  
        doc.fromHTML(newstr, 15, 15, {
            'width': 170,
                'elementHandlers': specialElementHandlers
        });
        doc.save('print_roster.pdf');
  

    




 
    }

    

    </script>

<script type="text/javascript">
function update_remark()
{

 var remark=document.getElementById('remark').value;
 var _date=document.getElementById('rdPRDate').value;
 var _outlet=document.getElementById('drpProject').value;
 var status='';
        $.ajax({
                      type: "POST",
                      url: "RetailRoaster.aspx/insert_remark",
                      data: "{'_remark':'"+remark+"','date':'"+_date+"','outlet':'"+_outlet+"' }",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      async: false,
                      success: function(msg) {
                        alert(msg);
                      },
                      error:function(msg) {
                      alert('Error:'+msg);
                
                      },
                       failure: function(msg) {
                        alert('Fail:'+msg);
                  
                      }
                    });
      
        //return 'YES';
        return false;
}


</script>



</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerPage" runat="server">
        </asp:ScriptManager>
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
            function Validate()
            {
           
               var lblDate=$("#lblFrom").text()
                if(lblDate=="-")
                {
                 alert("Please Select Date");
                 return false
                }
                //Date
               var project=document.getElementById("<%= drpProject.ClientID %>").value;
                if(project=="")
                {
                 alert("Please Select Outlet");
                 return false
                }
                
                return true;
            }
            </script>

        </radG:RadCodeBlock>
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Roster</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right" style="height: 25px">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <!-- header row -->
                                    <tr>
                                        <td align="left" class="bodytxt" style="width: 300px; ">
                                            <b>Choose any day to select week</b>
                                            <radG:RadDatePicker Width="100px" Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                ID="rdPRDate" runat="server" AutoPostBack="true">
                                                <DateInput ID="DIrdPRDate" runat="server" Skin="" DateFormat="dd/MM/yyyy" Enabled="false" />
                                                
                                                <%--     <ClientEvents OnDateSelected="OnDateSelected" />--%>
                                            </radG:RadDatePicker>
                                        </td>
                                        <td align="left" class="bodytxt">
                                            <b>From:&nbsp;&nbsp;</b><asp:Label ID="lblFrom" runat="server" Text="-"></asp:Label>
                                            <b>To:&nbsp;&nbsp;</b><asp:Label ID="lblTo" runat="server" Text="-"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <tt class="bodytxt">&nbsp;<b>Outlet :</b></tt>
                                            <%--       <select id="drpProject" runat="server" style="width: 160px" class="textfields" >
                                                <option selected="selected"></option>
                                            </select>--%>
                                            <asp:DropDownList ID="drpProject" runat="server" Style="width: 160px" class="textfields">
                                                <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid1" OnClientClick="return Validate()"
                                                runat="server" ImageUrl="~/frames/images/toolbar/go.ico" /></td>
                                        <td> <asp:Button ID="btnReport" runat="server" Text="Report" OnClick="btnReport_Click" /> </td>
                                        <td>
                                            <a href="../TimeSheet/Pattern.aspx"><b>Add Pattern</b></a></td>
                                        <td>
                                         <%--   <input type="button" value="Print" onclick="PrintElem('TableDiv')" /></td>--%>
                                            <input type="button" value="Print" id="cmd" /></td>
                                        <td>
                                            <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                                style="width: 80px; height: 22px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- End header row -->
                    </table>
                </td>
            </tr>
            <!------------- Main -->
            <tr>
                <td>
                    <div id="main_container1">
                        <!-- tables inside this DIV could have draggable content -->
                        <div id="drag">
                            <!-- left container -->
                            <div id="left">
                                <!-- new test  -->
                                <table cellpadding="0" cellspacing="0" border="0" style="background-color:White;">
                                    <tr>
                                        <td>
                                            <table style="width: 300px" cellpadding="0" cellspacing="0" id="TableHeader" runat="server"
                                                visible="false">
                                                <tr>
                                                    <td class="mark" style="background-color: #2452B0; ">
                                                        <font class="colheading"><b><font class="colheading"><b>Employee</b></font>
                                                            <input type="text" id="search" /></b></font>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="overflow: auto; height: 150px; width: 320px;">
                                                <table style="width: 300px;" cellpadding="0" cellspacing="0" id="table1">
                                                    <asp:Repeater ID="repeater1" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td class="single dark">
                                                                    <div id="<%# DataBinder.Eval(Container, "DataItem.emp_code")%>" class="drag  t3 clone"
                                                                        title="<%# DataBinder.Eval(Container, "DataItem.emp_code")%>" style="position: static;">
                                                                        <%# DataBinder.Eval(Container, "DataItem.Name")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </div>
                                        </td>
                                        <td valign="top">
                                           
                                            <asp:CheckBox ID="cbx15Times" Text="1.5 Times" CssClass="bodytxt"  runat="server" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <!-- end new test -->
                            </div>
                            <!-- left container -->
                            <div style="height: 10px;">
                            </div>
                            <!-- right container -->
                            <div id="right">
                                <div id="TableDiv" runat="server">
                                    <table id="tblPage" border="1">
                                    </table>
                                </div>
                                Remark:
                                <div>
                               <asp:TextBox runat="server" ID="remark" Columns="140" TextMode="multiline" Rows="15"  style="overflow:hidden"></asp:TextBox></div>
                               <br />
                               <input runat="SERVER" id="update_button" type="button" value="Save Remark" onclick ="update_remark()"/>
                            </div>
                         
                        </div>
                    </div>
                </td>
            </tr>
            <!----- end Main --->
        </table>
        <div id="editor"></div>
        
    </form>
    
</body>
</html>
