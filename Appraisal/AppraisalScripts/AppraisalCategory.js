$(document).ready(function () {
    //$('#sample_editable_1').dataTable({
    //    "columnDefs": [{
    //        "targets": 1,
    //        "searchable": false
    //    }]
    //});
    $.ajax({
        type: "POST",
        url: "AppraisalCategory.aspx/GetCategories",
        async: false,
        // data: JSON.stringify({ '_employeeOffDay': _object }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "";
            for (var i = 0; i < obj.datatb.length; i++) {
                str += '<tr><td>' + obj.datatb[i].CategoryName + ' </td><td style="display:none">' + obj.datatb[i].Id + '</td>';
                str += '<td class="text-center"><a class="edit" href="javascript:;"><i class="fa fa-pencil"></i></a></td>';
                str += '<td class="text-center"><a class="delete" href="javascript:;"><i class="fa fa-trash"></i></a></td>';
                str += '<td style="display:none"><a ></a></td>';
                str += '</tr>';

            }
            $(tbobjectives).html(str);



        },
        failure: function (response) {
            alert(response.d);
        }
    });
   
   
    $(".fa-plus").click(function () {
        $("#sample_2").dataTable().fnDestroy();
        var nRow = $(this).parents('tr')[0];
        var aData = nRow.children[1].innerHTML; // 1 is for 2nd column here which have Id hidden 
        
        $.ajax({
            type: "POST",
            url: "AppraisalCategory.aspx/GetObjectives",
            async: false,
            data: JSON.stringify({ CatId: aData }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response.d);
                var str = "";
                for (var i = 0; i < obj.datatb.length; i++) {
                    str += ' <tr><td class="text-center"><label class="mt-checkbox"><input type="checkbox" id="' + obj.datatb[i].Id + '"><span></span></label></td>'
                    str += ' <td>' + obj.datatb[i].CategoryName + ' </td><td> ' + obj.datatb[i].ObjectiveName;
                    str += ' </td><td>' + obj.datatb[i].ObjectiveType + ' </td><td hidden="true" style="display:none;">' + obj.datatb[i].Id + '</td></tr>';
                }
                $(tbvalues).attr("data-id",aData );
            $(tbvalues).html(str);
            TableDatatablesManaged.init();

            },
            failure: function (response) {
                alert(response.d);
            }



    });

    });


    $(btnSavemodal).click(function () {

        var jqInputs = $('input', tbvalues);
        var arrCheck = [];
        for (var i = 0; i < jqInputs.length; i++) {
            if(jqInputs[i].checked)
            {
                arrCheck.push(jqInputs[i].id);
            }

        }
        $.ajax({
            type: "POST",
            url: "AppraisalCategory.aspx/SaveObjectives",
            async: false,
            data: JSON.stringify({ArrID : arrCheck ,CatId : $(tbvalues).attr("data-id") }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var obj = JSON.parse(response.d);
               
                SuccessNotification("Objectives has been saved under this category");
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

        var table = $('#sample_2');

        // begin: third table
        datatable3 = table.dataTable({

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
                    "previous": "Prev",
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

        var tableWrapper = jQuery('#sample_2_wrapper');

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