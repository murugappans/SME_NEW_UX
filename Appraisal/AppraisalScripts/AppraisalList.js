$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "AppraisalList.aspx/GetAppraisals",
        async: false,
        // data: JSON.stringify({ '_employeeOffDay': _object }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "", selctedval = "";
           
            for (var i = 0; i < obj.datatb.length; i++) {
               
             
                str += '<tr><td>' + obj.datatb[i].AppraisalName + '</td>';
                str += '<td>' + dateFormat(obj.datatb[i].ValidFrom) + '</td><td>' + dateFormat(obj.datatb[i].ValidEnd) + '</td>';
                str += '<td class="text-center"><label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">';
                str += '<input type="checkbox" class="checkboxes clscheck" id="' + obj.datatb[i].AppraisalName + '" /><span></span></label></td></tr>';


            }
            $(tbvalues).html(str);

            TableDatatablesManaged.init();


        },
        failure: function (response) {
            alert(response.d);
        }
    });

    $(document).on('click', '.clscheck', function (e) {
        //e.preventDefault();

        //if (confirm("Are you sure you want to enable this Appraisal to all the Employees ?") == false) {
        //    return;
        //}
        var _this = $(this);

        var _dialog = window.confirm = function (message, callback, caption) {
            caption = caption || 'Approve Appraisal'
            $(document.createElement('div')).attr({
                title: caption,
                'class': 'dialog jscustom-dialog'
            }).html(message).dialog({
                position: ['center', 100],
                dialogClass: 'fixed',
                buttons: {
                    "Enable": function () {
                        $(this).dialog('close');
                        approveAppraisal(_this);
                        return true;
                    },
                    "Cancel": function () {
                        $(this).dialog('close');
                        $(_this).prop('checked', false);
                        return false;
                    }
                },
                close: function () {
                    $(this).remove();
                    $(_this).prop('checked', false);
                    return false;
                },
                draggable: true,
                modal: true,
                resizable: false,
                width: 'auto'
            });
        };

        confirm('Are you sure you want to enable this Appraisal to all the Employees?', function () {
            //what every needed to be done on confirmation has to be done here
            console.log('confirmed')
        })



        //return false;

      

    });

});
var datatable3;


function approveAppraisal(_this) {
    var nRow = _this.parents('tr')[0];
        var aData = datatable3.fnGetData(nRow);
        var DataId = $(_this).prop('id');

        $.ajax({
            type: "POST",
            url: "AppraisalList.aspx/EnableToUser",
            async: false,
            data: JSON.stringify({ AppName: DataId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var rply = response.d;
                if (rply == "false") {
                    WarningNotification("Can not Enable!! some error occured..");
                }
                else {
                    datatable3.fnDeleteRow(nRow);
                    SuccessNotification("Sent! this Appraisal form is sent to employee for self appraisal");
                }


            },
            failure: function (response) {
                WarningNotification(response.d);
            }
        });

}


function dateFormat(strdate)
{
    Dt = new Date(strdate);
    var yr = Dt.getFullYear();
    var dat = Dt.getDate();
    var month = Dt.getMonth()+1;

    return (dat <= 9 ? '0' + dat : dat) + '-' + (month <= 9 ? '0' + month : month) + '-' + yr ;
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
                [10, 25, 50, -1],
                [10, 25, 50, "All"] // change per page values here
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
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = jQuery('#sample_4_wrapper');

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