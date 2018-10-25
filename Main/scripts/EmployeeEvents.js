var _events = new Array();
$(document).ready(function () {

    var $calendar = $('#calendar');
    var t = this;
    t.someParam = false;
    $('#calendar').fullCalendar(
    {
        header:
                {
                    left: 'prev,next today title',
                    center: 'btnBirthday btnHoliday btnProbation btnPassport btnAllDocExpiry',
                    right: 'month,agendaWeek,agendaDay'
                },
        displayEventTime: false,
        defaultDate: Date.now(),
        selectable: true,
        selectHelper: true,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        select: function (start, end) {
            //AssignOffDay("", start.format('YYYY-MM-DD HH:mm'));

            //$("#formNewSchedule").show();
            $('#calendar').fullCalendar('unselect');
            //$('#rosterDate').val(start.format('D-MMM-YYYY'));
        },
        events: _events,
        customButtons: {
            btnBirthday: {
                text: 'Birthdays',
                click: function () {
                    addRemoveClassToheaders(this);
                    GetBirthdayList();
                }
            },
            btnHoliday: {
                text: 'Holidays',
                click: function () {
                    addRemoveClassToheaders(this);
                    GetPHList();
                }
            },
            btnProbation: {
                text: 'Probation Ends',
                click: function () {
                    addRemoveClassToheaders(this);
                    GetProbationPeriodExpiryList();
                }
            },
            btnPassport: {
                text: 'Passport Expiry',
                click: function () {
                    addRemoveClassToheaders(this);
                    GetPassportExpiryList();
                }
            },                          
            btnAllDocExpiry: {
                text: 'Documents Expiry',
                click: function () {
                    addRemoveClassToheaders(this);
                    GetAllDocsExpiryList();
                }
            }
        },
        eventRender: function (event, t, view) {
            //var dateString = moment(event.start).format('YYYY-MM-DD');
            t.hasClass("fc-day-grid-event") ? (t.data("content", e.description), t.data("placement", "top"), mApp.initPopover(t)) : t.hasClass("fc-time-grid-event") ? t.find(".fc-title").append('<div class="fc-description">' + e.description + "</div>") : 0 !== t.find(".fc-list-item-title").lenght && t.find(".fc-list-item-title").append('<div class="fc-description">' + e.description + "</div>")

        },
        eventClick: function (event, element) {
           // GetEventByID(event);

        },
        eventDrop: function (event, delta, revertFunc) {
            $('#mystate').val("Old")
            $('#rosterID').val(event.id);
            //dragSchedule(event, event.start.format('DD-MMM-YYYY HH:mm'));

        },
        
    });

    GetBirthdayList();



});


function GetPHList() {
    var _url = 'Home.aspx/GetPHList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#m_calendar").fullCalendar('removeEvents');
                $("#m_calendar").fullCalendar('addEventSource', _events);
                $("#m_calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}
function GetBirthdayList() {
    var _url = 'Home.aspx/GetBirthdayList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#m_calendar").fullCalendar('removeEvents');
                $("#m_calendar").fullCalendar('addEventSource', _events);
                $("#m_calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}
function GetProbationPeriodExpiryList() {
    var _url = 'Home.aspx/GetProbationPeriodExpiryList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#m_calendar").fullCalendar('removeEvents');
                $("#m_calendar").fullCalendar('addEventSource', _events);
                $("#m_calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}
function GetPassportExpiryList() {
    var _url = 'Home.aspx/GetPassportExpiryList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#m_calendar").fullCalendar('removeEvents');
                $("#m_calendar").fullCalendar('addEventSource', _events);
                $("#m_calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}
function GetAllDocsExpiryList() {
    var _url = 'Home.aspx/GetAllDocsExpiryList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#m_calendar").fullCalendar('removeEvents');
                $("#m_calendar").fullCalendar('addEventSource', _events);
                $("#m_calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}
function GetAllEventList() {
    var _url = 'Home.aspx/GetAllEventList';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_empid': _id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $('.loader').show();
        },
        success: function (data) {
            var obj = JSON.parse(data.d);

            setTimeout(function () {
                _events = obj;
                $("#m_calendar").fullCalendar('removeEvents');
                $("#m_calendar").fullCalendar('addEventSource', _events);
                $("#m_calendar").fullCalendar('rerenderEvents');

                $('.loader').hide();
            }, 200);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}

function addRemoveClassToheaders(_this) {
    $('#calendar').find('.fc-header-toolbar .fc-center .fc-button').removeClass('fc-state-active');
    $(_this).addClass('fc-state-active');
}