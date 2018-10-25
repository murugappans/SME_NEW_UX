$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "AppraisalTemplate.aspx/GetTemplates",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "", selctedval = "";

            for (var i = 0; i < obj.datatb.length; i++) {
                var dt = obj.datatb[i].Active == 1 ? "Active" : "Inactive";
               
                str += '<tr><td>' + obj.datatb[i].Name + '</td><td>' + dt + ' </td><td hidden="true" style="display:none;">' + obj.datatb[i].Id + '</td>';
                str += '<td class="text-center"><a class="edit" href="javascript:;"><i class="fa fa-pencil"></i></a> </td>';
                str += ' <td class="text-center"><a class="delete" href="javascript:;"><i class="fa fa-trash"></i></a></td>';
                str += ' <td class="text-center"><a data-toggle="modal" data-target="#myModal"><i class="fa fa-plus"></i></a></td></tr>';



                //     str += "<tr><td>" + obj.datatb[i].Name + "</td><td> " + dt + " </td><td hidden='true' style='display:none;'>" + obj.datatb[i].Id + "</td>";
                //str += "<td class='text-center'><a class='edit' href='javascript:;'><i class='fa fa-edit'></i>";
                //str += "</a></td><td class='text-center'><a class='delete' href='javascript:;'> ";
                //str += "<i class='fa fa-remove'></i></a> </td>";
                //str += '<td class="text-center"><a data-toggle="modal" data-target="#myModal"><i class="fa fa-plus"></i></a></td></tr>';
            }
            $(tbAppraisalTemplate).html(str);



        },
        failure: function (response) {
            alert(response.d);
        }


    });

    $(document).on('click', '.innerchk', function () {
        var chkbox = $(this).closest("table").find('input[type="checkbox"]');
        var chkdchkbox = $(this).closest("table").find('input[type="checkbox"]:checked');
        if (chkdchkbox.length == chkbox.length)
            $(this).closest("table").closest('tr').find('.clsCheck').prop('checked', true);
        else
            $(this).closest("table").closest('tr').find('.clsCheck').prop('checked', false);


    });


   // $(".clsCheck").click(function () {
    $(document).on('click', '.clsCheck', function () {
        var chkbox;
        var chkd = $(this)[0].checked;
            var cntrltable = $(this)[0].closest('tr').children[2].firstChild.firstChild;
            var NRows = cntrltable.children;
            for (var i = 0; i < NRows.length; i++) {
                chkbox = NRows[i].firstChild.firstChild.firstChild;
                if (chkd)
                {
                    $(chkbox).prop("checked", true);
                }
                else
                {
                    $(chkbox).prop('checked', false);
                }
            }
       


    });
    $(document).on('click', '.fa-plus', function () {
   // $(".fa-plus").click(function () {
        $("#sample_3").dataTable().fnDestroy();
      
        var nRow = $(this).parents('tr')[0];
        var aData = nRow.children[2].innerHTML; // 2 is for 2nd column here which have Id hidden 

        $.ajax({
            type: "POST",
            url: "AppraisalTemplate.aspx/GetObjectives",
            async: false,
            data: JSON.stringify({ TempId: aData }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response.d);
                var str = "";
                for (var i = 0; i < obj.dataCategories.length; i++) {


                    str += '<tr><td class="text-center"><label class="mt-checkbox "> <input type="checkbox" class="checkboxes clsCheck"/><span></span></label></td>';
                    str += '<td>'+ obj.dataCategories[i].CategoryName +' </td><td><table>';
                    for (var j = 0; j <  obj.dataAllObjectives.length; j++) {
                        if (obj.dataAllObjectives[j].CategoryID == obj.dataCategories[i].Id) {
                            str += '<tr><td><label class="mt-checkbox"><input type="checkbox" class="checkboxes innerchk" id=' + obj.dataAllObjectives[j].Id;

                            for (var k = 0; k < obj.dataObjectivesInTemplte.length; k++) {
                                if (obj.dataObjectivesInTemplte[k].Objective_Id == obj.dataAllObjectives[j].Id) {
                                    str += ' checked = "true"';
                                }
                               
                            }
                            str += ' />';
                            str += '<span></span></label></td><td>' + obj.dataAllObjectives[j].ObjectiveName + ' </td></tr>';
                        }
                     
                    }
                   

                    str +='</table></td></tr>';                  

                }
                $(tbobjectivelist).attr("data-id", aData);
                $(tbobjectivelist).html(str);
             
               
                TableDatatablesManaged.init();
            },
            failure: function (response) {
                alert(response.d);
            }



        });

    });

    $(btnsavemodal).click(function () {

        var jqInputs = datatable3.fnGetNodes();
        var jqtbody, jcntrl ;//$('input', sample_4);
        var arrCheck = [];
        for (var i = 0; i < jqInputs.length; i++) {
            jqtbody = $(jqInputs[i]).find('tbody')[0].children;
            for (var j = 0; j < jqtbody.length; j++) {
                jcntrl = $(jqtbody[j]).find('input')[0]
                if ($(jcntrl).is(":checked"))
                    arrCheck.push($(jcntrl).attr('id'));
            }
            
        }
        $.ajax({
            type: "POST",
            url: "AppraisalTemplate.aspx/SaveObjectives",
            async: false,
            data: JSON.stringify({ ArrID: arrCheck, TempId: $(tbobjectivelist).attr("data-id") }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response.d);

                SuccessNotification("Objectives has been saved under this Template");
                $('#myModal').modal('hide');

            },
            failure: function (response) {
                alert(response.d);
            }
        });


    });

});
var datatable3;
var TableDatatablesManaged = function () {

    var initTable3 = function () {

        var table = $('#sample_3');

        // begin: third table
        datatable3 =  table.dataTable({

            // Internationalisation. For more info refer to http://datatables.net/manual/i18n
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "bDestroy": true,
                "emptyTable": "No data available in table",
                "info": "Showing _START_ to _END_ of _TOTAL_ records",
                "infoEmpty": "No records found",
                "infoFiltered": "(filtered1 from _MAX_ total records)",
                "lengthMenu": "Show _MENU_",
                "search": "Search:",
                "zeroRecords": "No matching records found",
                "paginate": {
                    "previous":"Prev",
                    "next": "Next",
                    "last": "Last",
                    "first": "First"
                }
            },
            
            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "bStateSave": true, // save datatable state(pagination, sort, etc) in cookie.
            
            "lengthMenu": [
                [3, 15, 20, -1],
                [3, 15, 20, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 3,
            "columnDefs": [{  // set default column settings
                'orderable': false,
                'targets': [0]
            }, {
                "searchable": false,
                "targets": [0]
            }],
            "order": [
                [1, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = jQuery('#sample_3_wrapper');

        table.find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).prop("checked", true);
                } else {
                    $(this).prop("checked", false);
                }
            });
        });
    }

   
    return {

        //main function to initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }
            
            initTable3();
           
           
        }

    };

}();