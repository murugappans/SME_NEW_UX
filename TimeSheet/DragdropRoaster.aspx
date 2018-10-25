<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DragdropRoaster.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.DragdropRoaster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
            width: 850px;
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
            border: 2px solid #BF6A30;
            width:200px;
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
	width: 800px;
}
		/* container for the left table */
		#main_container #left{
			float: left;
			width: 100px;
			height: 200px;
		}
		/* container for the right table */
		#main_container #right{
		    float: right;
		  	display: table;
		    table-layout: auto;
			padding-left: auto;
			padding-right: 0px;
			margin-right: 20px;
			/* align div to the right */
			margin-left: auto;
			width:600px;
		}
    </style>
    <script src="../Frames/Script/jquery-1.3.2.min.js"" type="text/javascript"></script>
    <script src="../Frames/Script/redips-drag-min.js" type="text/javascript"></script>
   

    <script type="text/javascript">
        window.onload = function () 
        {
            // reference to the REDIPS.drag class
            var rd = REDIPS.drag;
            // initialization
            rd.init();
           
            // dragged elements can be placed to the empty cells only
				//rd.drop_option = 'multiple';
           
           

                // Handle dropped event
                rd.myhandler_dropped = function () 
                {
                    var obj = rd.obj, 		// current element
			        tac = rd.target_cell; // target cell
                     
                     var ddd= obj.id + ',' + tac.id;
                     //18,1_date
                    alert(ddd);
                    
                    
                    
                    
                 
                 /******* while drop- update in db ***********/ 
                  $.ajax({
                      type: "POST",
                      url: "DragdropRoaster.aspx/UpdateDB",
                        data: "{'val':'"+ddd+"'}",
                      contentType: "application/json; charset=utf-8",
                      dataType: "json",
                      success: function(msg) {
                        alert(msg);
                      },
                      error:function(msg) {
                        alert("error");
                      },
                       failure: function(msg) {
                        alert(msg);
                      }
                    });
                  
                /******* End while drop- update in db ***********/
                 
                 }
                                                
            };


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManagerPage" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upPage" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="main_container">
                    <!-- tables inside this DIV could have draggable content -->
                    <div id="drag">
                        <!-- left container -->
                        <div id="left">
                            <table id="Employee List" width="100%">
                                <colgroup>
                                    <col width="100" />
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <td class="mark">
                                            Employee
                                        </td>
                                    </tr>
                                    <% if (EmployeeListAray != null)
                                       {

                                           for (int i = 0; i < EmployeeListAray.Count; i++)
                                           {
                                               SMEPayroll.TimeSheet.EmployeeList opaylist = (SMEPayroll.TimeSheet.EmployeeList)EmployeeListAray[i];  
                                    %>
                                    <tr style="background-color: #eee">
                                        <td class="single dark" >
                                            <div id="Div2" class="drag  t3 clone" title="<%=opaylist.Emp_code%>" style="position: static;">
                                                <%=opaylist.Name%>
                                            </div>
                                        </td>
                                    </tr>
                                    <%
                                        }
                                    }%>
                                </tbody>
                            </table>
                        </div>
                        <!-- left container -->
                        <!-- right container -->
                        <div id="right">
                        <div id="TableDiv" runat="server">
                           <table id="tblPage" runat="server" ></table>
                            
                            </div>
                            
                           		<div class="trash" title="Trash">Trash</div>
                            
                        </div>
                    </div>
                    
                        <table style="height:90px;width:90px;">
                           <tr style="background-color: #eee">
					       <td class="trash" title="Trash" height="30px">Trash</td>
				         </tr> 
                        </table>
                    	
      
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
        
    </form>
</body>
</html>
