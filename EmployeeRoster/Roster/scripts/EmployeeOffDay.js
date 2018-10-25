var _events = new Array();
$(document).ready(function () {
    //$('#modelDateLabel').text(' ' + moment().format('D MMM YYYY'));
    GetEmployees();
    //GetEmployeesOffList();
    var $calendar = $('#calendar');
    var t = this;
    t.someParam = false;
    $('#calendar').fullCalendar(
    {
        header:
                {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
        displayEventTime: false,
        defaultDate: Date.now(),
        selectable: true,
        selectHelper: true,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        select: function (start, end) {
            AssignOffDay("", start.format('YYYY-MM-DD HH:mm'));

            //$("#formNewSchedule").show();
            $('#calendar').fullCalendar('unselect');
            //$('#rosterDate').val(start.format('D-MMM-YYYY'));
        },
        events: _events,
        loading: function (boo,view) {
            if (!bool) {
                AppendFilters();
            }
            //Possibly call you feed loader to add the next feed in line
        },
        eventRender: function (event, element, view) {
            var dateString = moment(event.start).format('YYYY-MM-DD');
            // $('#calendar').find('.fc-day-number[data-date="' + dateString + '"]').css('background-color', '#36c6d3');
            //setEventColorCodes(event.TrainingStatus, element);


            // $(element).tooltip({ title: event.title + ': ' + event.employeeName });

        },
        eventClick: function (event, element) {
            GetEventByID(event);
            //event.title = "CLICKED!";
        },
        eventDrop: function (event, delta, revertFunc) {
            $('#mystate').val("Old")
            $('#rosterID').val(event.id);
            dragSchedule(event, event.start.format('DD-MMM-YYYY HH:mm'));

        },//,timeFormat: 'H:mm'
        ///////////////////////////////////////////////////////////////////////////////

        eventMouseover: function (data, event, view) {

            tooltip = '<div class="event-tooltip">' + 'Name: ' + data.employeeName + '</br>' + 'Title: ' + data.title + '</br>' + 'Phone: ' +  data.mobile + '</br>' + 'Off day: ' + data.start.format('DD-MMM-YYYY') + '</div>';


            $("body").append(tooltip);
            $(this).mouseover(function (e) {
                $(this).css('z-index', 10000);
                $('.event-tooltip').fadeIn('500');
                $('.event-tooltip').fadeTo('10', 1.9);
            }).mousemove(function (e) {
                $('.event-tooltip').css('top', e.pageY + 10);
                $('.event-tooltip').css('left', e.pageX + 20);
            });


        },
        eventMouseout: function (data, event, view) {
            $(this).css('z-index', 8);

            $('.event-tooltip').remove();

        },
        dayClick: function () {
            //tooltip.hide()
        },
        eventResizeStart: function () {
            tooltip.hide()
        },
        eventDragStart: function () {
            tooltip.hide()
        },



        ////////////////////////////////////////////////////////////////////////////////////

    });

    //////////////////////////////////////////////////////////////////

    $('#selectEmployeeList').change(function () {
        var _id = $(this).val();
        GetEmployeesOffListByEmployeeID(_id);

    });

    //////////////////////////////////////////////////////////////////

    $(document).on('click', '#all_day', function () {
        var _selected = $(this).is(":checked");
        if (_selected)
            $('.time-block').addClass('hidden');
        else
            $('.time-block').removeClass('hidden');

    });


    $(document).on('click', '.cancel', function () {
        $('#formNewSchedule').hide();
    });


    $(document).on('click', '#btnDeleteEvent', function () {
        DeleteEvent();
    });



    $(document).on('click', '#btnSaveSchedule', function () {
        UpdateEvent();
    });


    $(document).on('keyup', '#Remarks', function () {
        var _currentval = $("#Remarks").val().length;
        if (_currentval > 300)
            return false;
        $("#lblCharacters").text(300 - _currentval) + "Characters left";
    });
    $(document).on('keypress', '#Remarks', function () {
        var _currentval = $("#Remarks").val().length;
        if (_currentval > 300)
            return false;
    });
    $(document).on('focusout', '#Remarks', function () {
        var _currentval = $("#Remarks").val().length;
        if (_currentval > 300) {
            var _val = $("#Remarks").val();
            var res = _val.slice(1, 300);
            $("#Remarks").val(res);
            $("#lblCharacters").text(0);
        }
    });

});


function AssignOffDay(_id, _date, _eventStatus) {
    if ($("#selectEmployeeList").val() === '-1') {
        WarningNotification('No employee is selected.');
        return true;
    }
    var _url = 'EmployeeOffDay.aspx/SaveNewRoseter';
    var _object = new Object();
    _object.employeeID = $("#selectEmployeeList").val();
    _object.employeeName = $("#selectEmployeeList>option:selected").text();
    _object.startTime = _date;
    _object.endTime = _date;
    _object.title = $("#selectEmployeeList>option:selected").text();
    _object.description = "";
    _object.offDateFrom = _date;
    _object.offDateTo = _date;

    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_employeeOffDay': _object }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            //_events = obj;
            _events.push(obj);

            //_events=[{ "id": "2302", "title": "XXX", "start": "2017/12/05 12:00:00", "end": "2017/12/05 12:00:00", "allDay": true, "url": "xxx" }]

            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');
            var _messageType = obj.returnMessage.split('|')[0] === "Warning" ? WarningNotification(obj.returnMessage.split('|')[1]) : SuccessNotification(obj.returnMessage.split('|')[1]);

        },
        failure: function (response) {
            alert(response.d);
        }
    });
}




function GetEmployees() {
    //$("#formNewSchedule").show(); 
    var _url = 'EmployeeOffDay.aspx/GetEmployees';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_viewBy': _viewBy }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var _data = JSON.parse(data.d);

            var procemessage = "<option value='0'> Please wait...</option>";
            $("#selectEmployeeList").html(procemessage).show();
            var markup = '<option value="-1">--select employee--</option>';

            for (var x = 0; x < _data.length; x++) {
                markup += "<option value=" + _data[x].mainID + ">" + _data[x].Description + "</option>";
            }


            $("#selectEmployeeList").html(markup).show();



        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}

function GetEmployeesOffList() {
    var _url = 'EmployeeOffDay.aspx/GetEmployeeOffList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var obj = JSON.parse(data.d);
            _events = obj;
            AppendFilters();

        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}

function GetEmployeesOffListByEmployeeID(_id) {
    var _url = 'EmployeeOffDay.aspx/GetEmployeeOffListByEmployeeID';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#calendar").fullCalendar('removeEvents');
                $("#calendar").fullCalendar('addEventSource', _events);
                $("#calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}

function dragSchedule(_event, _date) {
    var _url = 'EmployeeOffDay.aspx/DragRoster';
    var employeeroster = new Object();
    employeeroster.offDayID = _event.id;
    employeeroster.offDateFrom = _date;
    employeeroster.employeeID = $("#selectEmployeeList").val();
       $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_employeeOffDay': employeeroster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
             var obj = JSON.parse(data.d);
             _events = obj;

             $("#calendar").fullCalendar('removeEvents');
             $("#calendar").fullCalendar('addEventSource', _events);
             $("#calendar").fullCalendar('rerenderEvents');
             var _messageType = obj[0].returnMessage.split('|')[0] === "Warning" ? WarningNotification(obj[0].returnMessage.split('|')[1]) : SuccessNotification(obj[0].returnMessage.split('|')[1]);

            
             //SuccessNotification(obj[0].returnMessage);
            //setTimeout(function () {
            //    $('.loader').hide();
            //}, 1400);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function GetEventByID(_event) {
    //$("#formNewSchedule").show();
    $("#btnmodal").click();
    var _url = "";
    _url = 'EmployeeOffDay.aspx/GetEventByID';
    var roster = new Object();
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_id': _event.id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (_data) {
            var obj = JSON.parse(_data.d);
            $('#mystate').val("Old");
            $('#offDayID').val(obj.id);           
            $('#title').val(obj.title);
            $('#offDate').val(obj.offFrom);
            $('#startTime').val(obj.start);
            $('#endTime').val(obj.end);
            $('#description').val(obj.description);
            $('#modelDateLabel').text(obj.offFrom);
            initDatePicker();
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function UpdateEvent(_id, _date, _eventStatus) {
    var _url = 'EmployeeOffDay.aspx/UpdateEvent';
    var empOff = new Object();

    empOff.employeeID = $("#selectEmployeeList").val();
    empOff.offDayID = $('#offDayID').val();
    empOff.description = $('#description').val();
    empOff.title = $('#title').val();
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_employeeOffDay': empOff }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var obj = JSON.parse(data.d);
            _events=obj;
            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');
            SuccessNotification("Event updated successfully.");
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}


function DeleteEvent(_id, _date, _eventStatus) {
    var _url = 'EmployeeOffDay.aspx/DeleteEvent';
    var empOff = new Object();
    empOff.employeeID = $("#selectEmployeeList").val();
    empOff.offDayID = $('#offDayID').val();
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_employeeOffDay': empOff }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var obj = JSON.parse(data.d);
            _events = obj;
            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');
            SuccessNotification("Event deleted successfully.");
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}


function AppendFilters() {
    var _viewbyFilters = $('#viewbyFilters').length;
    if (_viewbyFilters > 0) return true
    var _filtersection =
   ['<div class="form-group" id="viewbyFilters">',
                                //'<label class="col-md-1 control-label label-viewby">View by</label>',
                                '<div class="col-md-5">',
                                    '<select id="selectViewBy" class="form-control">',
                                        '<option value="">View by</option><option value="Project">Project</option><option value="Team">Team</option></select>',
                                '</div>',
                                '<div class="col-md-5">',
                                   '<select id="selectViewByList" class="form-control"><option value=""></option></select>',
                                '</div>',
                            '</div>'
   ].join('\n');

    $('.fc-header-toolbar .fc-left *:first').before(_filtersection);
    //$('.fc-header-toolbar .fc-left').append(_filtersection);
}


function initDatePicker() {
    $('#offDate').removeAttr("value");
    $('#offDate').attr("value", $('#offDate').val());
    $('input[id="offDate"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'DD-MMM-YYYY'
        }
    });


}