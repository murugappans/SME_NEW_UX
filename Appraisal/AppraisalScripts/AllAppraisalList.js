$(document).ready(function () {
    if (sessionStorage.getItem('Rplymsg') || sessionStorage.getItem('Rplymsg') != "")
    {
        SuccessFixedNotification(sessionStorage.getItem('Rplymsg'));
        sessionStorage.setItem('Rplymsg', "");
    }
    $.ajax({
        type: "POST",
        url: "AppraisalAllList.aspx/GetAppraisals",
        async: false,
        // data: JSON.stringify({ '_employeeOffDay': _object }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "", selctedval = "";

            for (var i = 0; i < obj.datatb.length; i++) {
                obj.datatb[i].Designation = obj.datatb[i].Designation == null ? "" : obj.datatb[i].Designation;

                str += '<tr><td>' + obj.datatb[i].AppraisalName + '</td><td>' + obj.datatb[i].EmployeeName + '</td><td>' + obj.datatb[i].Designation + '</td>';
                str += '<td>' + dateFormat(obj.datatb[i].ValidFrom) + '</td>';
                str += '<td>' + dateFormat(obj.datatb[i].ValidEnd) + '</td>';
                str +=  (obj.datatb[i].EnbleToEmployee == 1) ? "<td>YES</td>" : "<td>NO</td>";
                str += '<td>' + obj.datatb[i].Status + '</td>',
                // str += '<td class="text-center"><a class="view" href="../Appraisal/ManagerAppraisal.aspx?id=' + obj.datatb[i].Id + '"><i class="fa fa-reply"></i></a></td>';
                str+='</tr>';



            }
            $(tbvalues).html(str);

            TableDatatablesManaged.init();


        },
        failure: function (response) {
            alert(response.d);
        }
    });
});
var datatable3;
function dateFormat(strdate) {
    Dt = new Date(strdate);
    var yr = Dt.getFullYear();
    var dat = Dt.getDate();
    var month = Dt.getMonth() + 1;

    return (dat <= 9 ? '0' + dat : dat) + '-' + (month <= 9 ? '0' + month : month) + '-' + yr;
}
var TableDatatablesManaged = function () {


    var initTable3 = function () {

        var table = $('#sample_3');

        // begin: third table
        datatable3 = table.dataTable({

            // Internationalisation. For more info refer to http://datatables.net/manual/i18n
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
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
                [20, 40, 50, -1],
                [20, 40, 50, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 20,
            "columnDefs": [{  // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": true,
                "targets": [0]
            }],
            "order": [
                [0, "asc"]
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