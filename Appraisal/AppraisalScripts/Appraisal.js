
var arrResponseId = []; var arrChecked = [];

$(document).ready(function () {
   
    $.ajax({
        type: "POST",
        url: "Appraisal.aspx/GetTemplates",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var selectedval, obj = JSON.parse(response.d);
            var optionsHTML = [];
            optionsHTML.push('<option value="-1">Select Template</option>');
            for (var i = 0; i < obj.dataTemplates.length; i++) {
                optionsHTML.push('<option value="' + obj.dataTemplates[i].Id + '">' + obj.dataTemplates[i].Name + '</option>');
                
            }
            $(dpTemplate).append(optionsHTML.join('\n'));
           
        },
        failure: function (response) {
            WarningNotification(response.d);
        }

    });
   
    $(txtDaysApprove).keyup(function () {
        var st = $(txtDaysApprove).val();
        if (st.indexOf('.') > -1) {
            WarningNotification("Number of Days for approval can not be a decimal value. ");
            $(txtDaysApprove).val("");
        }
        if (st.indexOf('-') > -1) {
            WarningNotification("Number of Days for approval can not be negative . ");
            $(txtDaysApprove).val("");
        }
        if (st== "") {
            WarningNotification("Invalid Number of Days for Approval ");
            $(txtDaysApprove).val("");
        }
       
    });
    $(txtDaysApprove).focusout(function () {
        var st = $(txtDaysApprove).val();
        if (st.indexOf('.'))
        {

        }
        ChekApprovalDays();      

    });
    $(txtfromDate).change(function () {
        checkDate();
        ChekApprovalDays();

    });
    $(txtToDate).change(function () {
       
        ChekApprovalDays();

    });

    $(dpTemplate).change(function () {
        GetTemplateObjectives();
        
    });
    $(dpEmployeeOrDept).change(function () {
        GetDeptEmployees();
       
    });

    var arrId = [], allemployees = [];

  
    $('#btnSavemodal').on('click', function (event) {
        $('#myModal').modal('hide');
       
            if (arrResponseId.length > 0) {
                $.each(arrResponseId, function (ind2, val2) {

                    $.each(arrId, function (ind3, val3) {
                        if (val2 != null)
                            if (val2.EmpId === val3)
                                arrId.splice(ind3, 1);
                    });

                    $.each(allemployees, function (ind4, val4) {
                        if (val2 != null && val4)
                            if (val2.EmpId === val4.EmpId) {
                                allemployees.splice(ind4, 1);

                            }
                    });


                });
            }
            if (arrId.length > 0) {
                checkAppraisalExist(allemployees, arrId);
            }
            else {
                WarningNotification("There are no employees left to add appraisal.. Please select other employees. ");
                return false;
            }
       
        

    });

  
    $('#btnCancelmodalapp').on('click', function (event) {
        $('#myModalapp').modal('hide');
       
            if (arrResponseId.length > 0) {
                $.each(arrResponseId, function (ind, val) {

                    $.each(arrId, function (index, value) {
                        if (val != null)
                            if (val.EmpId === value)
                                arrId.splice(index, 1);
                    });

                    $.each(allemployees, function (indx, valu) {
                        if (val != null && valu)
                            if (val.EmpId === valu.EmpId) {
                                allemployees.splice(indx, 1);

                            }
                    });


                });
            }

            if (arrId.length > 0) {
               
                    SaveAppraisalFinally(arrId);
               
            }
            else {
                WarningNotification("There are no employees left to add appraisal.. Please select other employees. ");
                return false;
            }
     

    });

    $('#btnSavemodalapp').on('click', function (event) {

            SaveAppraisalFinally(arrId);
    });

   

    
var  ConfirmWf = false;
    $('#btnSaveAppraisal').click(function () {
     
       
        if ($(txtName).val() == "") {
            WarningNotification("Please enter Name for the Appraisal");
            $(txtName).focus();
            return false;
        }
        if ($(txtDaysApprove).val() == "") {
            WarningNotification("Please insert number of Approval Days at a level for the Appraisal");
            $(txtDaysApprove).focus();
            return false;
        }
        if ($(txtfromDate).val() == "") {
            WarningNotification("Please select Starting date for the Appraisal");
            $(txtfromDate).focus();
            return false;
        }
        if ($(txtToDate).val() == "") {
            WarningNotification("Please select Ending date for the Appraisal");
            $(txtToDate).focus();
            return false;
        }
        if ($(dpEmployeeOrDept).val() == "-1") {
            WarningNotification("Please select Asignee for the Appraisal");
            $(dpEmployeeOrDept).focus();
            return false;
        }
        if ($(dpTemplate).val() == "-1") {
            WarningNotification("Please select Template for the Appraisal");
            $(dpTemplate).focus();
            return false;
        }
       
      
      
        var jqInputs = datatable5.fnGetNodes();
        var Confirm = false;
        var msgstr = "";
        var  jcntrl;
        for (var i = 0; i < jqInputs.length; i++) {
            jcntrl = $(jqInputs[i].firstChild).find('input')[0];
            if ($(jcntrl).is(":checked"))
            {
                    arrId.push($(jcntrl).attr('id'));
                    
            }
        }
        if (arrId.length < 1)
        {
            WarningNotification("Please select " + $(dpEmployeeOrDept).val() + " for the Appraisal");
            return false;
        }
        else {
            $("#sample_2").dataTable().fnDestroy();
           
                $.ajax({
                    type: "POST",
                    url: "Appraisal.aspx/WorkflowExist",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ Emp: arrId, Type: $(dpEmployeeOrDept).val() }),
                    success: function (response) {
                        var selectedval, obj = JSON.parse(response.d); var str="";
                        arrId = [];
                        $.each(obj.AllSelectedEmployee, function (ind3, val3) {
                           
                            arrId[ind3] = val3.EmpId;
                        });
                        allemployees = obj.AllSelectedEmployee;
                        if (obj.IsWFEnabled && obj.isFound) {
                            arrResponseId = obj.NotSeletedIds;
                           
                            $.each(obj.NotSeletedIds, function (ind, val) {
                                if(val != null)
                                    str += "<tr><td>" + val.TypeName + "</td> <td>" + val.EmpName + "</td> <td>" + val.EmpId + "</td> </tr>"

                            });
                            $("#hdmodaltxt").text('Appraisal for the following employees cannot be initiated as their supervisior in the workflow has not been assigned. If you want to initiate the appraisal for rest of the employees press "OK" else press "Cancel".');
                            $("#tbvalues").html(str);
                            TableDatatablesManaged3.init();
                            $('#myModal').data('id',"Workflow")
                            $('#myModal').modal('show');
                            
                           
                           
                        }
                        else
                        {
                            if (allemployees.length > 0)
                                checkAppraisalExist(allemployees, arrId);  // call if Wf is disabled
                            else
                            {
                                WarningNotification("There are no employees in the Team/Department.. Please select other Team/Department. ");
                                return false;
                            }
                           
                        }




                    },
                    failure: function (response) {
                        WarningNotification(response.d);
                    }

                });
           


           
          
        }
       

        });

    $(document).on('click','.clsCheck',function(){
    //$(".clsCheck").click(function () {
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
    $(".btn-info-custom").click(function () {
        InfoNotification("Person on each level must reply to the appraisal within the following days.");
    });
});


function checkAppraisalExist(allSelected,arrId)
{
    var inputval = $(txtfromDate).val().split("/");
    var d1 = new Date(inputval[1] + "/" + inputval[0] + "/" + inputval[2]);
    $("#sample_9").dataTable().fnDestroy();
    

    arrResponseId = [];
    $.ajax({
        type: "POST",
        url: "Appraisal.aspx/AppExists",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Emp: allSelected, Validfrom: d1 }),
        success: function (response) {
            var selectedval, obj = JSON.parse(response.d);

            $.each(obj.Ids, function (index, val) {
                if (val) {
                    arrResponseId[index] = val;


                }
            });
            if (obj.IsFound) {
                var str = "";
                $.each(arrResponseId, function (ind, val) {
                    if (val != null)
                        str += "<tr><td>" + val.TypeName + "</td> <td>" + val.EmpName + "</td> <td>" + val.EmpId + "</td> </tr>"

                });
                $("#hdmodalapptxt").text('There is already an Appraisal Inprogress for this period for the following employees.If you want to initiate this appraisal for all the selected employees including the mentioned employees then press "OK" else press "Cancel". \n \n (Note : Pressing "Cancel" will submit appraisal for all the employees except the mentioned employees.)');
                $("#tbappvalues").html(str);
                TableDatatablesManaged9.init();
              //  $('#myModal').data('id', "AppExist")
                $('#myModalapp').modal('show');

                //if (confirm('There is already an Appraisal Inprogress for this period for the some employees.<a>(List of employees)</a> If you want to initiate this appraisal for all these employees then press "OK" else press "Cancel". \n (Note : Pressing "Cancel" will submit appraisal for all the employees except the mentioned employees.)')) {
                //    Confirm = true;
               // }
            }
            else
            {
            
                SaveAppraisalFinally(arrId);
            }



        },
        failure: function (response) {
            WarningNotification(response.d);
        }

    });
   
}
function SaveAppraisalFinally(arrId) {
    var tempstr = "";
    var inputval = $(txtfromDate).val().split("/");
    var d1 = new Date(inputval[1] + "/" + inputval[0] + "/" + inputval[2]);
    inputval = $(txtToDate).val().split("/");
    var d2 = new Date(inputval[1] + "/" + inputval[0] + "/" + inputval[2]);
    if (arrChecked.length > 0) {
        var jqInputs = datatable4.fnGetNodes();
        var arrObjId = [];
        var jcntrl;
        for (var i = 0; i < jqInputs.length; i++) {
            jcntrl = $(jqInputs[i].firstChild).find('input')[0];
            if ($(jcntrl).is(":checked"))
                arrObjId.push($(jcntrl).attr('id'));
        }
        if (arrObjId.length < 1) {
            WarningNotification("No Objective have been selected for the Appraisal");
            return false;
        }

        tempstr = "<br/><br/>Note : As you have unchecked some objectives from the selected template, this template will be saved as new Template with the name of : Template Used in Appraisal '" + $(txtName).val() + "'";
    }

    var enabled = 0;
    if ($(dpEnabled).is(":checked")) {
        enabled = 1;
    }

    var Obj = {
        EmpId: arrId,
        AppraisalName: $(txtName).val(),
        AppraisalTemplateID: $(dpTemplate).val(),
        DaysToApproveLevel: $(txtDaysApprove).val(),
        EnbleToEmployee: enabled,
        ValidEnd: d2,
        ValidFrom: d1,
    }



    var NewTemplate = {
        TemplateName: "Template Used in Appraisal '" + $(txtName).val() + "'",
        ObjectiveId: arrObjId
    };




    $.ajax({
        type: "POST",
        url: "Appraisal.aspx/SaveAppraisal",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Appraisal: Obj, NewTemplate: NewTemplate }),
        success: function (response) {
            var selectedval, obj = JSON.parse(response.d);
            if (obj.done) {
                SuccessNotification(obj.rply);
                sessionStorage.setItem('Rplymsg', (obj.rply + tempstr));
                window.location.replace("../Appraisal/AppraisalAllList.aspx");
            }
            else
                WarningNotification(obj.rply);

        },
        failure: function (response) {
            WarningNotification(response.d);
        }

    });


}
function checkDate()
{
    var inputval = $(txtfromDate).val().split("/");
    var d1 = new Date(inputval[1] + "/" + inputval[0] + "/" + inputval[2]);

    //inputval = $(txtToDate).val().split("/");
    var d2 = new Date();
    d2 = new Date(d2.getMonth()+1 + "/" + d2.getDate() + "/" + d2.getFullYear());
    if (d1 < d2)
    {
        WarningNotification("Staring day of Appraisal should not be less than Today");
        $(txtfromDate).val("");
        $(txtToDate).val("");
    }
}
function ChekApprovalDays()
{
    var inputval = $(txtfromDate).val().split("/");
    var d1 = new Date(inputval[1] + "/" + inputval[0] + "/" + inputval[2]);

    inputval = $(txtToDate).val().split("/");
    var d2 = new Date(inputval[1] + "/" + inputval[0] + "/" + inputval[2]);

    var oneDay = 24 * 60 * 60 * 1000;
    var diff = 0;
    if (d1 && d2) {

        diff = Math.round(Math.abs((d2.getTime() - d1.getTime()) / (oneDay)));
    }
    if ($(txtDaysApprove).val()!="" && $(txtDaysApprove).val() > (diff+1)) {
        WarningNotification("Number of days for approval are exceeding the total number of days to complete the Appraisal. ");
        $(txtDaysApprove).val("");
        return false;
    }
    if ($(txtDaysApprove).val() != "" && $(txtDaysApprove).val() < 1) {
        WarningNotification("Number of days for approval cannot be less than 1 ");
        $(txtDaysApprove).val("");
        return false;
    }
}
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
                str += '</td><td>' + obj.dataTemplates[i].CategoryName + '</td><td>' + obj.dataTemplates[i].ObjectiveName + ' </td></tr>';
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
    //if (selectedval == '-1') {
    //    $('#sample_5').hide();
    //    $('#sample_5').dataTable().fnDestroy();
    //    return false;
    //}
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
                str += '</td><td style="white-space:pre">' + obj.dataEmployees[i].EmpName + ' </td></tr>';
            }
            //$(tbtemplatesObjectives).attr("data-id", aData);
            $(thEmployeeOrDept)[0].innerHTML = selectedval == -1 ? "" : selectedval;
            $(tbDeptOrEmployee).html(str);
            $(sample_5).show();
            TableDatatablesManaged5.init();
           
        },
        failure: function (response) {
            WarningNotification(response.d);
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

var datatable3;
var TableDatatablesManaged3 = function () {

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

var datatable9;
var TableDatatablesManaged9 = function () {

    var initTable9 = function () {

        var table = $('#sample_9');

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

        var tableWrapper = jQuery('#sample_9_wrapper');

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

            initTable9();


        }

    };

}();
