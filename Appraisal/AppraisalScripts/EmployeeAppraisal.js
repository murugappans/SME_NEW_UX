$(document).ready(function () {
    var queryString = document.location.search;
    var parts = queryString.split('=');
    var value = parts[1];
    $.ajax({
        type: "POST",
        url: "EmployeeAppraisal.aspx/GetAppraisalForm",
        async: false,
        data: JSON.stringify({ 'AppraisalId': value }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var str = "", count = 0;
            var obj = JSON.parse(response.d);
            if (!obj) {
                $('#txtMainLabel')[0].textContent = "No Apprasial for you has been initiated for this session Or You hav already submitted your feedback";
                $(btnSaveappraisal).attr("disabled", "disabled");
            }
            else {
                $('#txtMainLabel')[0].textContent = obj.dtAppraisal[0].AppraisalName;
                $('#txtMainLabel').attr("data-id", obj.dtAppraisal[0].Id);

                for (var i = 0; i < obj.dtAppraisalObjectives.length; i++) {
                    count++;
                    str += "<tr><td>" + count + "</td><td> " + obj.dtAppraisalObjectives[i].ObjectiveName + " </td><td>";



                    switch (obj.dtAppraisalObjectives[i].ObjectiveType) {
                        case "Rating5":
                            str += ' <div class="stars" id="' + obj.dtAppraisalObjectives[i].Id + '">';
                            str += ' <input value="5" class="star star-5" id="star-5-2' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starfive' + obj.dtAppraisalObjectives[i].Id + '" />';
                            str += '<label class="star star-5" for="star-5-2' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                            str += ' <input value="4" class="star star-4" id="star-4-2' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starfive' + obj.dtAppraisalObjectives[i].Id + '" />';
                            str += ' <label class="star star-4" for="star-4-2' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                            str += '<input value="3" class="star star-3" id="star-3-2' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starfive' + obj.dtAppraisalObjectives[i].Id + '" />';
                            str += '<label class="star star-3" for="star-3-2' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                            str += ' <input value="2" class="star star-2" id="star-2-2' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starfive' + obj.dtAppraisalObjectives[i].Id + '" />';
                            str += ' <label class="star star-2" for="star-2-2' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                            str += '<input value="1" class="star star-1" id="star-1-2' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starfive' + obj.dtAppraisalObjectives[i].Id + '" />';
                            str += '<label class="star star-1" for="star-1-2' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                            str += ' </div>';
                            break;

                        case "Rating10":
                            {
                                str += '<div class="stars" id="' + obj.dtAppraisalObjectives[i].Id + '">';
                                str += '<input value="10" class="star star-5" id="star-10-10' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<label class="star star-5" for="star-10-10' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += '<input value="9" class="star star-4" id="star-10-9' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<label class="star star-4" for="star-10-9' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += '<input value="8" class="star star-3" id="star-10-8' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<label class="star star-3" for="star-10-8' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += '<input value="7" class="star star-2" id="star-10-7' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<label class="star star-2" for="star-10-7' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += '<input value="6" class="star star-1" id="star-10-6' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<label class="star star-1" for="star-10-6' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += '<input value="5" class="star star-1" id="star-10-5' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += ' <label class="star star-1" for="star-10-5' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += ' <input value="4" class="star star-1" id="star-10-4' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += ' <label class="star star-1" for="star-10-4' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += ' <input value="3" class="star star-1" id="star-10-3' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += ' <label class="star star-1" for="star-10-3' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += ' <input value="2" class="star star-1" id="star-10-2' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += ' <label class="star star-1" for="star-10-2' + obj.dtAppraisalObjectives[i].Id + '"></label>';
                                str += ' <input value="1" class="star star-1" id="star-10-1' + obj.dtAppraisalObjectives[i].Id + '" type="radio" name="starten' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<label class="star star-1" for="star-10-1' + obj.dtAppraisalObjectives[i].Id + '"></label>';

                                str += '  </div>';
                                break;
                            }
                        case "YESorNO":
                            {
                                str += ' <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">';
                                str += '<input type="checkbox" class="checkboxes" id="' + obj.dtAppraisalObjectives[i].Id + '" />';
                                str += '<span></span>';
                                str += '</label>';
                                break;
                            }
                        case "Percentage":
                            {
                                str += ' <input type="number" id="' + obj.dtAppraisalObjectives[i].Id + '" class="form-control input-sm" />';
                                break;
                            }
                        case "PFGVE":
                            {
                                str += '<div class="mt-radio-inline" id="' + obj.dtAppraisalObjectives[i].Id + '">';
                                str += '<label class="mt-radio mt-radio-outline">';
                                str += '<input name="optionsRadios' + obj.dtAppraisalObjectives[i].Id + '" id="optionsRadios5" value="Poor" checked="" type="radio">Poor';

                                str += '<span></span></label>';
                                str += '<label class="mt-radio mt-radio-outline">';
                                str += '<input name="optionsRadios' + obj.dtAppraisalObjectives[i].Id + '" id="optionsRadios4" value="Fair" type="radio">Fair';

                                str += '<span></span></label>';
                                str += '<label class="mt-radio mt-radio-outline">';
                                str += '<input name="optionsRadios' + obj.dtAppraisalObjectives[i].Id + '" id="optionsRadios3" value="Good" type="radio">Good';

                                str += '<span></span></label>';
                                str += '<label class="mt-radio mt-radio-outline">';
                                str += '<input name="optionsRadios' + obj.dtAppraisalObjectives[i].Id + '" id="optionsRadios2" value="Very Good" type="radio">Very Good';

                                str += '<span></span> </label>';
                                str += '<label class="mt-radio mt-radio-outline">';
                                str += '<input name="optionsRadios' + obj.dtAppraisalObjectives[i].Id + '" id="optionsRadios1" value="Excellent" type="radio">Excellent';

                                str += '<span></span> </label></div>';
                                break;

                            }
                        default:

                    }

                    str += '</td><td><textarea rows="2" class="form-control input-sm" id="' + obj.dtAppraisalObjectives[i].Id + '"></textarea></td></tr>';

                    // str += "<tr><td>" + obj.dtAppraisalObjectives[i].CategoryName + "</td><td> " + obj.dtAppraisalObjectives[i].ObjectiveName + " </td><td> " + selctedval + " </td><td hidden='true' style='display:none;'>" + obj.dtAppraisalObjectives[i].Id + "</td>";


                }
            }
            
           
           
            
            $(tbvalues).html(str);
            TableDatatablesManaged.init();


        },
        failure: function (response) {
            alert(response.d);
        }
    });

    $(btnSaveappraisal).click(function () {

        event.preventDefault();
        var EmployAppraisalRply = [];
        var varDiv,GroupName, strrply = "", strremark = "", strId = "", RadioCheckedOfthisGroup = false;
       
        var Nodes = datatable3.fnGetNodes();
        var arrTextArea = [] ;
        var arrInputs = [] ;
       
        var inp;
        for (var x = 0; x < Nodes.length; x++) {
            RadioCheckedOfthisGroup = false;
            arrInputs = $(Nodes[x]).find('input');
            arrTextArea = $(Nodes[x]).find('textarea');


            for (var i = 0; i < arrInputs.length; i++) {
                var type = $(arrInputs[i]).prop("type");

                
                if (type == "radio") {

                   
                    if (arrInputs[i].checked) {
                        strrply = $(arrInputs[i]).val();
                        varDiv = $(arrInputs[i]).closest('div');
                        strId = varDiv[0].id;
                        strremark = $(arrTextArea[0]).val();
                        RadioCheckedOfthisGroup = true;
                        
                    }
                    if (i == (arrInputs.length-1) && !RadioCheckedOfthisGroup) {
                        alert("Please rate youself");
                        $(arrInputs[i]).focus();
                        return false;
                    }
                   


                }
                else {
                    if (type == "checkbox") {
                        strrply = arrInputs[i].checked ? "Yes" : "No";
                        strId = $(arrInputs[i]).attr("id");
                        strremark = $(arrTextArea[0]).val();
                       
                    }
                    else {
                        strId = $(arrInputs[i]).attr("id");
                        if ($(arrInputs[i]).val() == "" || $(arrInputs[i]).val() < 1 || $(arrInputs[i]).val() > 100) {
                            alert("Please enter some numeric value between 0-100");
                            $(arrInputs[i]).focus();
                            return false;
                        }
                        strrply = $(arrInputs[i]).val();
                        strremark = $(arrTextArea[0]).val();
                       
                    }
                }




                if (strrply != "" && strId != "" && i < arrInputs.length) {
                    var Appraisaldata =
                                   {
                                       Remark: strremark,
                                       Comment: strrply,
                                       ObjectiveId: strId

                                   };
                    EmployAppraisalRply.push(Appraisaldata);
                    strrply = ""; strremark = ""; strId = "";
                }

            }
        }

            $.ajax({
                type: "POST",
                url: "EmployeeAppraisal.aspx/FeedbackSend",
                async: false,
                data: JSON.stringify({ EmployAppraisalRply, AppraisalId: $('#txtMainLabel').attr("data-id")}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var obj = JSON.parse(response.d);
                    if (obj > 0)
                    {
                        SuccessNotification("Your Feedback has been sent");
                        window.location.replace("../Appraisal/MyAppraisalList.aspx");
                    }


                },
                });

    });

});
var datatable3;
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
                [10, 20, 30, -1],
                [10, 20, 30, "All"] // change per page values here
            ],
            // set the initial value
            "pageLength": 10,
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