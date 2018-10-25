$(document).ready(function () {
    dte = new Date();
    $(txtYear).val( dte.getFullYear());
    var arrChecked = [];
    $.ajax({
        type: "POST",
        url: "Appraisal.aspx/GetTemplates",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var selectedval, obj = JSON.parse(response.d);
            var optionsHTML = [];
            for (var i = 0; i < obj.dataTemplates.length; i++) {
                optionsHTML.push('<option value="' + obj.dataTemplates[i].Id + '">' + obj.dataTemplates[i].Name + '</option>');
                
            }
            $(dpTemplate).append(optionsHTML.join('\n'));
           
        },
        failure: function (response) {
            alert(response.d);
        }

    });
    GetTemplateObjectives();
    GetDeptEmployees();
   
    $(dpTemplate).change(function () {
        GetTemplateObjectives();
        
    });
    $(dpEmployeeOrDept).change(function () {
        GetDeptEmployees();
       
    });

    $(btnSaveAppraisal).click(function () {
        event.preventDefault();
        var arrId = [], arrObjId = [];       
        var jqInputs = datatable5.fnGetNodes();
        
        var  jcntrl;
        for (var i = 0; i < jqInputs.length; i++) {
            jcntrl = $(jqInputs[i].firstChild).find('input')[0];
                if ($(jcntrl).is(":checked"))
                    arrId.push($(jcntrl).attr('id'));
        }

        if (arrId.length < 1)
        {
            alert("Please select " + $(dpEmployeeOrDept).val() + " for the Appraisal");
            return false;
        }
        if ($(txtName).val() == "") {
            alert("Please type Name for the Appraisal");
            $(txtName).focus();
            return false;
        }
        if ($(txtDaysApprove).val() == "")
        {
            alert("Please insert number of Appvoval Days at a level for the Appraisal");
            $(txtDaysApprove).focus();
            return false;
        }
        if ($(txtfromDate).val() == "") {
            alert("Please select From date for the Appraisal");
            $(txtfromDate).focus();
            return false;
        }
        if ($(txtToDate).val() == "") {
            alert("Please select " + $(dpEmployeeOrDept).val() + " for the Appraisal");
            $(txtToDate).focus();
            return false;
        }
        if (arrChecked.length > 0)
        {           
            var jqInputs = datatable4.fnGetNodes();

            var jcntrl;
            for (var i = 0; i < jqInputs.length; i++) {
                jcntrl = $(jqInputs[i].firstChild).find('input')[0];
                if ($(jcntrl).is(":checked"))
                    arrObjId.push($(jcntrl).attr('id'));
            }
            if (arrObjId.length < 1) {
                alert("No Objective have been selected for the Appraisal");
                return false;
            }
            
            alert("Note : As you have unchecked some objectives from the selected template, this template will be saved as new Template with the name of : Template Used in Appraisal '" + $(txtName).val() + "'");
        }

        var NewTemplate = {
            TemplateName: "Template Used in Appraisal '" + $(txtName).val() + "'",
            ObjectiveId: arrObjId
        }
        var Obj = {
            EmpiD : arrId,
            AppraisalTemplateID: $(dpTemplate).val(),           
            DaysToApproveLevel: $(txtDaysApprove).val(),
            EnbleToEmployee: $(dpEnabled).val(),
            Perid: $(dpPeriod).val(),
            AppraisalYear: $(txtYear).val(),
            AppraisalName: $(txtName).val(),
            Status: $(dpAppraisalStatus).val(),
            WFLevel:"",
            ValidFrom: $(txtfromDate).val(),
            ValidEnd: $(txtToDate).val()

        };

        $.ajax({
            type: "POST",
            url: "Appraisal.aspx/SaveAppraisal",
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ Appraisal: Obj, type: $(dpEmployeeOrDept).val(), NewTemplate }),
            success: function (response) {
                var selectedval, obj = JSON.parse(response.d);
                if (obj.done)
                {
                    SuccessNotification(obj.rply);
                }
                else {

                    WarningNotification2(obj.rply);
                }

            },
            failure: function (response) {
                alert(response.d);
            }

        });

        });

    //$(document).on('click','.clsCheck',function(){
    $(".clsCheck").click(function () {
        var cntrl = $(this)[0].firstChild;
        var present = false;
        var checked = $(cntrl).is(":checked");
        if (!checked)
        {
            for (var i = 0; i < arrChecked.length; i++) {
                if (arrChecked[i] == $(cntrl).attr('id'))
                {
                    present = true;
                }
               
            }
            if(!present)
            {
                arrChecked.push($(cntrl).attr('id'));
            }
            
        }
        else {
            for (var i = 0; i < arrChecked.length; i++) {
                if (arrChecked[i] == $(cntrl).attr('id')) {
                    arrChecked.splice(i, 1);
                }
            }

        }

    });
});
var datatable5;
var datatable4;
function GetTemplateObjectives()
{
    var selectedval = $(dpTemplate).val();
    $('#sample_4').dataTable().fnDestroy();
    //var nRow = $(this).parents('tr')[0];
   // var aData = nRow.children[2].innerHTML; // 2 is for 2nd column here which have Id hidden 

    $.ajax({
        type: "POST",
        url: "Appraisal.aspx/GetTemplateObjectives",
        async: false,
        data: JSON.stringify({ TempId: selectedval }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "";
            for (var i = 0; i < obj.dataTemplates.length; i++) {
                str += ' <tr><td class="text-center"><label class="mt-checkbox mt-checkbox-single mt-checkbox-outline clsCheck">';
                str += '<input type="checkbox" class="checkboxes " id="' + obj.dataTemplates[i].Id + '" checked="true" /><span></span></label>';
                     str += '</td><td>' + obj.dataTemplates[i].ObjectiveName + ' </td></tr>';
            }
            //$(tbtemplatesObjectives).attr("data-id", aData);
            $(tbtemplatesObjectives).html(str);
            $(sample_4).show();
            TableDatatablesManaged4.init();
            
        },
        failure: function (response) {
            alert(response.d);
        }



    });
}
function GetDeptEmployees() {
    var selectedval = $(dpEmployeeOrDept).val();
    $('#sample_5').dataTable().fnDestroy();
    //var nRow = $(this).parents('tr')[0];
    // var aData = nRow.children[2].innerHTML; // 2 is for 2nd column here which have Id hidden 

    $.ajax({
        type: "POST",
        url: "Appraisal.aspx/GetEmployees",
        async: false,
        data: JSON.stringify({ Type: selectedval }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            var str = "";
            for (var i = 0; i < obj.dataEmployees.length; i++) {
                str += ' <tr><td class="text-center"><label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">';
                str += '<input type="checkbox" class="checkboxes" id="' + obj.dataEmployees[i].Id + '"  /><span></span></label>';
                str += '</td><td>' + obj.dataEmployees[i].EmpName + ' </td></tr>';
            }
            //$(tbtemplatesObjectives).attr("data-id", aData);
            $(thEmployeeOrDept)[0].innerHTML=selectedval;
            $(tbDeptOrEmployee).html(str);
            $(sample_5).show();
            TableDatatablesManaged5.init();
           
        },
        failure: function (response) {
            alert(response.d);
        }



    });
}
var TableDatatablesManaged4 = function () {


    var initTable4 = function () {

        var table = $('#sample_4');

        // begin: third table
        datatable4 =  table.dataTable({

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
                [3, 6, 9, -1],
                [3, 9, 24, "All"] // change per page values here
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

            initTable4();
           


        }

    };

}();
var TableDatatablesManaged5 = function () {   
   
var initTable5 = function () {

    var table = $('#sample_5');

    // begin: third table
    datatable5 =  table.dataTable({

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
            [6, 15, 20, -1],
            [6, 15, 20, "All"] // change per page values here
        ],
        // set the initial value
        "pageLength": 6,
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

    var tableWrapper = jQuery('#sample_5_wrapper');

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

            
            initTable5();


        }

    };

}();

