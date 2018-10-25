var isEmployeeFilled = false;
$(document).ready(function () {
    $('input[id="dateFrom"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'D-MMM-YYYY'
        }
    });

    $('input[id="dateTo"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'D-MMM-YYYY'
        }
    });

    $(document).on('change', '#selectViewBy',function(){
        var _selected = $(this).val();
        if (_selected === "")
            $('#spanfilters').addClass('hidden');
        else
            $('#spanfilters').removeClass('hidden');

        if (_selected === "Period") {
            $('.select-employee').addClass('hidden');
            $('.period-section').removeClass('hidden');
        }
        else {
            if (isEmployeeFilled == false)
                GetEmployeeList();
            $('.select-employee').removeClass('hidden');
            $('.period-section').addClass('hidden');
        }
    });

    $(document).on('change', '#selectViewByList2', function () {
        var _selected = $('#selectViewBy').val();
        //if (_selected === "Employee") {
            $('.loader').show();
            setTimeout(function () {
                GetEmployeeRosterList();
            }, 800);
        //}
    });

    //$(document).on('change', '#dateFrom,#dateTo', function () {
    //    $('.loader').show();
    //    setTimeout(function () {
    //        GetEmployeeRosterList();
    //    }, 800);
    //});


    $(document).on('click', '#btnGo', function () {
        var _fromDate = $('#rosterDate').val();
        var _toDate = $('#rosterDateTo').val();
        _fromDate = Date.parse(_fromDate);
        _toDate = Date.parse(_toDate);
        if (_toDate < _fromDate) {
            WarningNotification("To date should be equal or greater than from date.");
            
           return false;
        }
     
        $('.loader').show();
        setTimeout(function () {
            GetEmployeeRosterList();
        }, 800);


    });

});


function GetEmployeeList() {
    var _url = 'EmployeeOffList.aspx/GetEmployeeList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_viewBy': _viewBy }), 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var _data = JSON.parse(data.d);

            if (_data.length <= 0) {
                $("#selectViewByList").html("");
                $('.loader').hide();
                return true;
            }

            var procemessage = "<option value='0'> Please wait...</option>";
            $("#selectViewByList").html(procemessage).show();
            var markup = '<option value="-1">--select employee--</option>';
            //var _data = _allProducts != "" ? data._sortedList : data._sortedList2;
            for (var x = 0; x < _data.length; x++) {
                markup += "<option value=" + _data[x].mainID + ">" + _data[x].Description + "</option>";
            }

            //if (_callfrom === "ViewBy")
            //    $("#selectViewByList").html(markup).show();
            //else
            $("#selectViewByList").html(markup).show();
            isEmployeeFilled = true;
            //$("#selectViewByList").val($('#hdnID').val());
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}


function GetEmployeeRosterList() {
    var _url = 'EmployeeOffList.aspx/GetEmployeeOffList';
    var empoff = new Object();
    var _viewby = "";
    empoff.employeeID = $('#selectViewByList').val();
    if ($('#selectViewBy').val() === "Period") {
        empoff.filterType = "Period";
        empoff.offDateFrom = $('#dateFrom').val();
        empoff.offDateTo = $('#dateTo').val();
    }
    else if ($('#selectViewBy').val() === "Employee") {
        empoff.filterType = "Employee";
    }

    else
        return true;


    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ 'empoff': empoff }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend:function(){
            $('.loader').show();
        },
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj.length <= 0) {
                $('#InputWorkerAttendance tbody').text("No record found.");
                $('.loader').hide();
                return true;
            }

            var objevent = JSON.parse(response.d);

            $('#InputWorkerAttendance tbody').text("");
            $.each(objevent, function (key, val) {
                // _events[key].title = _events[key].eventTitle;
                var _tr =
            ['<tr>',
            '<td>' + val.employeeName + '</td>',
            '<td>' + val.startTime + '</td>',
            '<td>' + val.endTime + '</td>',
            '<td>' + val.phone + '</td>',
            '<td>' + val.description + '</td>',
            '</tr>'
            ].join('\n');
            $('#InputWorkerAttendance tbody').append(_tr);
           });



            setTimeout(function () {
                $('.loader').hide();
            }, 1400);

        },
        failure: function (response) {
            alert(response.d);
        }
    });
}