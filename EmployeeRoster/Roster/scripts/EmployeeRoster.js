var _events = new Array();
var _resources = [];
$(document).ready(function () {
    $('#modelDateLabel').text(' '+ moment().format('D MMM YYYY'));
    var $calendar = $('#calendar');
    var t = this;
    t.someParam = false;
    $('#calendar').fullCalendar(
    {
        header: {
            left: 'addRemoveResource today prev,next',
            center: 'title',
            right: 'timelineDay,month,customWeek'
            //right: 'agendaDay,timelineDay,agendaWeek,month,customWeek'
        },
         displayEventTime: false,
        defaultDate: Date.now(),
        resourceAreaWidth: 230,
        selectable: true,
        selectHelper: true,
        editable: true,
        aspectRatio: 1.5,
        scrollTime: '00:00',
        minTime: '07:00',
        maxTime: '31:00',
        eventLimit: true, // allow "more" link when too many events
        //filterResourcesWithEvents:true,
        //filterResourcesWithEvents: function () {
        //  _resources= _emptyResources;
        //    return true;
        //},
        select: function (start, end) {
            if (ValidateFieldsBeforeSaving("beforeSave") == false)
                return false;
            $('#btnSaveSchedule').text("Publish");
            $('#btnDeleteEvent').addClass("hidden");
            loadNewSchedule("", start);
           
            $('#calendar').fullCalendar('unselect');
            $('#rosterDate').val(start.format('D-MMM-YYYY'));
            $('#rosterDateTo').val(start.format('D-MMM-YYYY'));
            initDatePicker();
        },
        loading: function (bool) {
            if (!bool) {
                AppendFilters();
            }
            //Possibly call you feed loader to add the next feed in line
        },
        events: _events,
        eventRender: function (event, element, view) {
            var dateString = moment(event.start).format('YYYY-MM-DD');
            // $('#calendar').find('.fc-day-number[data-date="' + dateString + '"]').css('background-color', '#36c6d3');
            setEventColorCodes(event.TrainingStatus, element);

            //$(element).tooltip({ title: event.title });

        },
        eventClick: function (event, element) {
            $('#hdnendTime').val("");
            $('#hdnstartTime').val(event.start.format('DD-MMM-YYYY HH:mm'));
            if (event.end)
                $('#hdnendTime').val(event.end.format('DD-MMM-YYYY HH:mm'));
            $('#btnSaveSchedule').text("Update");
            $('#btnDeleteEvent').removeClass("hidden");
            GetEventByID(event);
            //loadNewSchedule(event.id,"", event.TrainingStatus);
            //event.title = "CLICKED!";
        },
        eventResize: function(event, delta, revertFunc) {
            $('#mystate').val("Old")
            $('#mainID').val(event.id);
           // ResizeSchedule(event, event.end.format('DD-MMM-YYYY HH:mm'));
            var _wholeseriesDialog = window.confirm = function (message, callback, caption) {
                caption = caption || 'Update confirmation'
                $(document.createElement('div')).attr({
                    title: caption,
                    'class': 'dialog jscustom-dialog'
                }).html(message).dialog({
                    position: ['center', 100],
                    dialogClass: 'fixed',
                    buttons: {
                        "Whole series": function () {
                            $(this).dialog('close');
                            ResizeSchedule(event, event.end.format('DD-MMM-YYYY HH:mm'), true);
                            $(this).addClass('btn red');
                            return true;
                        },
                        "Selected only": function () {
                            $(this).dialog('close');
                            ResizeSchedule(event, event.end.format('DD-MMM-YYYY HH:mm'), false);
                            $(this).addClass('btn default');
                            return false;
                        }
                    },
                    close: function () {
                        $(this).remove();
                        $("#calendar").fullCalendar('removeEvents');
                        $("#calendar").fullCalendar('addEventSource', _events);
                        $("#calendar").fullCalendar('rerenderEvents');
                        return false;
                    },
                    draggable: true,
                    modal: true,
                    resizable: false,
                    width: 'auto'
                });
            };

            confirm('Choose the below button to complete the action. If you click selected only, this will make it different series.', function () {
                //what every needed to be done on confirmation has to be done here
                console.log('confirmed')
            })
        },
        eventDrop: function (event, delta, revertFunc) {
            if (event.timestart == event.start._i) {
                WarningNotification("Same day not acceptable.");
                $("#calendar").fullCalendar('removeEvents');
                $("#calendar").fullCalendar('addEventSource', _events);
                $("#calendar").fullCalendar('rerenderEvents');
                return false;
            }
            $('#mystate').val("Old")
            $('#mainID').val(event.id);

            var _wholeseriesDialog = window.confirm = function (message, callback, caption) {
                caption = caption || 'Update confirmation'
                $(document.createElement('div')).attr({
                    title: caption,
                    'class': 'dialog jscustom-dialog'
                }).html(message).dialog({
                    position: ['center', 100],
                    dialogClass: 'fixed',
                    buttons: {
                        "Whole series": function () {
                            $(this).dialog('close');
                            dragSchedule(event, event.start.format('DD-MMM-YYYY HH:mm'),true);
                            return true;
                        },
                        "Selected only": function () {
                            $(this).dialog('close');
                            dragSchedule(event, event.start.format('DD-MMM-YYYY HH:mm'),false);
                            return false;
                        }
                    },
                    close: function () {
                        $(this).remove();
                        $("#calendar").fullCalendar('removeEvents');
                        $("#calendar").fullCalendar('addEventSource', _events);
                        $("#calendar").fullCalendar('rerenderEvents');
                        return false;
                    },
                    draggable: true,
                    modal: true,
                    resizable: false,
                    width: 'auto'
                });
            };

            confirm('Choose the below button to complete the action. If you click selected only, this will make it different series.', function () {
                //what every needed to be done on confirmation has to be done here
                console.log('confirmed')
            })



            //dragSchedule(event, event.start.format('DD-MMM-YYYY HH:mm'));

          //saveNewRoster();
            //if (!confirm("Are you sure about this change?")) 
            //    revertFunc();            

        },//,timeFormat: 'H:mm'
        defaultView: 'customWeek',
        views: {
            timelineDay: {
                buttonText: 'Day'
            },
            month: {
                buttonText: 'Month'
            },
            timelineThreeDays: {
                type: 'timeline',
                duration: { days: 3 }
            }, customWeek: {
                type: 'timelineWeek',
                slotDuration: '24:00:00',
                //duration: { days: 7 },
                buttonText: 'Week'
            }
        },
        resourceColumns: [
      {
          labelText: 'Employees',
          field: 'title',
          render: function (resource, el) {
       
              //el.css({
              //    'background-color': 'black',
              //    'color': '#fff',
              //    'font-size': '12px',
              //    'width': '160px'
              //});

              el.addClass("employee-resourse");
          }
      },
      {
          labelText: 'Hours',
          field: 'start_end',
          render: function (resource, el) {
              if (t.someParam) {
                  alert('aa');
              }
                 el.addClass("time-resourse");
              //if (resource.time) {
              //el.css({
              //    'background-color': 'black',
              //    'color':'#fff',
              //    'font-size': '12px',
              //    'width':'60px'
              //});
           
              //}
          }
      }
        ],
        resources: function (callback) {
            if (t.someParam) {
                EmployeeList = _resources//[{ id: 1, title: 'It is TRUE!' }, { id: 2, title: 'Resource 2' }]
            }
            else {
                EmployeeList = _resources; //[{ id: 3, title: 'It is False!' }, { id: 4, title: 'Resource 4' }]
            }
            callback(EmployeeList);
            $($('.fc-widget-header').find('.fc-cell-text')[0]).text("Employees");
        },

        ///////////////////////////Tooltip Start/////////////////////////////////////

        eventMouseover: function (data, event, view) {

           // var _emplist = "";
            //$.each(data.employeeList)
           // $.each(data.employeeList[0].map((o) => o.employeeName), function (key, val) {

                //_emplist = _emplist + ", " + val;
            //});


            //data.employeeList[0].employeeName


            tooltip = '<div class="event-tooltip">' + 'Title:  ' + data.title + '</br>' +
                'Start:  ' + data.start.format('DD-MMM-YYYY hh:mm a') + '</br>' +
                'End:  ' + data.end.format('DD-MMM-YYYY hh:mm a') + '</br>' +
                'Description:  ' + data.description + '</br>' +
                'Project:  ' + data.projectName + '</br>' +
                'Team:  ' + data.teamName + '</br>' +
                //'Employees :' + _emplist

                '</div>';


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
        eventResizeStart: function (data, event, view) {
            //tooltip.hide()
        },
        eventDragStart: function (data, event, view) {
            //tooltip.hide()
            var _aa = data;
        },
        /////////////////////////Tooltip End///////////////////////////

    });


    $(document).on('click', '.fc-prev-button,.fc-next-button', function () {
        $('#selectViewByList').change();
    });


    //$(document).on('click', '.fc-next-button', function () {
    //    $('#selectViewByList').change();
    //});



    //////////////////////////////////////////////////////////////////
    $(document).on('change', '#selectViewBy',function(){
        var _selected = $(this).val();
        if (_selected === "") {
            $("#selectViewByList").addClass("hidden");
            return false;
        }
        $("#selectViewByList").removeClass("hidden");
        GetProjects(_selected, 'ViewBy');
        return true;

        if (_selected === "Project") {
            _resources = [];

            _resources = [
        { id: '', title: '', occupancy: 40 },
            ]
            //LoadRosters();
            $('#calendar').fullCalendar('refetchEvents');
            //t.someParam = !t.someParam;
            $('#calendar').fullCalendar('refetchResources');
        }
    });

    $(document).on('change', '#selectViewByList', function () {
        var _selected = $('#selectViewBy').val();
        if ($('#selectViewByList').val() === "")
        {
            $('.loader').hide();
            _resources = [];
            _events = [];
            $("#calendar").fullCalendar('removeEvents');
            $('#calendar').fullCalendar('refetchEvents');
            $('#calendar').fullCalendar('refetchResources');
            return false;
        }
        else if (_selected === "Project")
            _selected = "Team";
        else if (_selected === "Employee")
            _selected = "Project";
        else
            _selected = "Project";


        GetProjects("Project","Project");

           $('.loader').show();
            setTimeout(function () {
        
            GetTeamProject(_selected, 'ViewByItem');
            _resources = [];

            _resources = [
        { id: '', title: '', occupancy: 40 },
            ]
            GetEmployeeRosterList();
        }, 800);
           
            $('#calendar').fullCalendar('refetchEvents');
            //t.someParam = !t.someParam;
            $('#calendar').fullCalendar('refetchResources');
        
    });
    //$(document).on('click', '#Team_Employee', function () {   
    //    var _selected = $('input[name=Team_Employee]:checked').val();
    //    GetTeamProject(_selected, 'ViewByItem');

    //});
   //////////////////////////////////////////////////////////////////

    var _startT = "";
    var _endT = "";
    $(document).on('click', '#all_day', function () {
        var _selected = $(this).is(":checked");
        if (!_startT && !_endT)
        {
            _startT = $('#startTime').val();
            _endT = $('#endTime').val();
        }
        if (_selected) {
            $('#startTime').val('00:00:00');
            $('#endTime').val('23:59:59');
            //$('.time-block').addClass('hidden');
        }

        else {
            $('#startTime').val(_startT);
            $('#endTime').val(_endT);
            // $('.time-block').removeClass('hidden');
        }

    });

    $(document).on('click', '.cancel', function () {
        $('#formNewSchedule').hide();
    });
   
    $(document).on('change', '#StartTime2', function () {
        //var _fullval = $(this).val().split(' ')[0];
        //var _hours = _fullval.split(':')[0];
        //var _minutes = _fullval.split(':')[1];
        //$('#_EndTime').val(_hours + 1 + ': ' + _minutes)
        operateTime();
    });

    $(document).on('click', '#btnSaveSchedule', function () {
        saveNewRoster();
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
    //////////////////////////////////////////////////////////
    var initialText = $('.editable').val();
$('.editOption').val(initialText);

$('#selectTeamProject').change(function () {
    var selected = $('option:selected', this).attr('class');
    var _assignedto = $('input[name=Team_Employee]:checked').val();
    var _selectedType = _assignedto === "Team" ? "selected-team" : "selected-employee";
    var _tootip = _assignedto === "Team" ? "Team" : "Employee";
   // var _labeltag = '<p class="s">'+_selectedType+' <p/>'

    var optionText = $("#selectTeamProject option:selected").text();
    var _val = $(this).val();

var _chooseagain = $('.span-emp[data-val=' + _val + ']').length;
if (_chooseagain > 0) return true;
var _multiselect =
['<span class="span-emp ' + _selectedType + '" data-type=' + _assignedto + ' data-val=' + _val + '>' + optionText,
                        '<span class="fa fa-times remove-emp"><span/>',
                        '</span>',
                      
].join('\n');
if ($('.editOption').find('.span-emp[data-type=Team]').length > 0 && _assignedto==='Team') {
    WarningNotification('Only one team can be selected.');
    _multiselect = "";
}

$('.editOption').prepend(_multiselect + " ").show();

$('.emp-list-box-viewer').removeClass('fa-plus');
$('.emp-list-box-viewer').addClass('fa-minus');

});
    //////////////////////////////////////////////////////////
$(document).on('click', '.remove-emp', function () {
    $(this).parent('span').remove();
    if ($('.editOption span').length < 1) {
        $('.editOption').hide();
        $('.emp-list-box-viewer').removeClass('fa-minus');
        $('.emp-list-box-viewer').addClass('fa-plus');
    }
    //$(this).remove();
});

$(document).on('click', '.editOption', function () {
    $('#selectTeamProject').click();
    //$(this).remove();
});

$(document).on('click', '.emp-list-box', function () {
    $('.editOption').hide();
    //$(this).remove();
});
$(document).on('click', '.emp-list-box-viewer', function () {
    var _thisclass = "";
    if ($(this).hasClass('fa-minus')) {
        $(this).removeClass('fa-minus');
        $(this).addClass('fa-plus');
        $('.editOption').hide();
    }
    else {
        $(this).removeClass('fa-plus');
        $(this).addClass('fa-minus');
        $('.editOption').show();
    }
});

$(document).on('click', '.commomsettings-show', function () {
    $('.js-common-settings').removeClass('hidden');
    $('.commomsettings-hide').removeClass('hidden');
    $('.commomsettings-show').addClass('hidden');
});

$(document).on('click', '.commomsettings-hide', function () {
    $('.js-common-settings').addClass('hidden');
    $('.commomsettings-show').removeClass('hidden');
    $('.commomsettings-hide').addClass('hidden');

});
    
$(document).on('click', '.topsection-show', function () {
    $('.js-topsection').removeClass('hidden');
    $('.topsection-hide').removeClass('hidden');
    $('.topsection-show').addClass('hidden');
});

$(document).on('click', '.topsection-hide', function () {
    $('.js-topsection').addClass('hidden');
    $('.topsection-show').removeClass('hidden');
    $('.topsection-hide').addClass('hidden');

});

$(document).on('click', '#btnDeleteEvent', function () {
    DeleteEvent();
});

$(document).on('click', '#btnDeleteEventsecond', function () {
    GetConfirmation("Are you sure you want to delete this event?", 'btnDeleteEvent', "Confirm Delete", "Delete");
});

$(document).on('change', '#rosterDate,#rosterDateTo', function () {
    var _fromDate = $('#rosterDate').val();
    var _toDate = $('#rosterDateTo').val();
    _fromDate = Date.parse(_fromDate);
    _toDate = Date.parse(_toDate);
    if (_toDate < _fromDate) {
        WarningNotification("To date should be equal or greater than from date.");
        if ($('#mystate').val() === "Old")
            $('#rosterDateTo').val($('#hdnRosterDateTo').val());
        else
            $('#rosterDateTo').val($('#rosterDate').val());
        return false;
    }
    $('#shiftEnd').val($('#rosterDateTo').val());
    $('#shiftEndTime').val($('#endTime').val());
});

$(document).on('change', '#endTime', function () {
    $('#shiftEnd').val($('#rosterDateTo').val());
    $('#shiftEndTime').val($('#endTime').val());

    if (validateTime() == false)
        WarningNotification("End time cannot be less than or equal to start time in case of single day.");
});

});

//$.sessionTimeout({
//    warnAfter: 12000,
//    redirAfter: 1200000
//});
function renderEvents () {
    return _events;
}

function dragSchedule(_event, _date,_wholeseries) {
    var _url = 'RosterBox.aspx/DragRoster';
    var employeeroster = new Object();

   // var _wholeseries = confirm("Change whole series?")
    if (_wholeseries == true) {
        employeeroster.filterType = "WholeSeries";
        employeeroster.seriesID = _event.seriesID;;
    }
    employeeroster.referenceRosterID = _event.rosterID;

    employeeroster.id = $('#mainID').val();
   employeeroster.rosterID = _event.rosterID;
    employeeroster.teamID = _event.teamID;
    employeeroster.projectID = _event.projectID;
    employeeroster.rosterDate = _date;
    employeeroster.employeeID = _event.employeeID;
    employeeroster.startTime = _event.start.format('DD-MMM-YYYY HH:mm');
    if (_event.end==null)
        employeeroster.endTime = _event.timeend;
    else
        employeeroster.endTime = _event.end.format('DD-MMM-YYYY HH:mm');
    employeeroster.displayFilterType = $('#selectViewBy').val();
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ 'roster': employeeroster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj == "SessionTimeout")
                window.location.replace("../SessionExpire.aspx");
            if (obj.length <= 0) {
                _resources = _emptyResources;
                $('#calendar').fullCalendar('refetchResources');
                $('.loader').hide();
                return true;
            }

            var objevent = JSON.parse(response.d);
            _resources = obj;
            _resources.push(_emptyResources);
            $('#calendar').fullCalendar('refetchResources');
            _events = [];
            _events = objevent;
            $('#calendar').fullCalendar('gotoDate', objevent[0].start);
            $.each(_events.map((o) => o.title), function (key, val) {
                _events[key].title = _events[key].eventTitle;
            });

            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');
            // SuccessNotification("Schedule updated successfully.");
            var _messageType = obj[0].returnMessage.split('|')[0] === "Warning" ? WarningNotification(obj[0].returnMessage.split('|')[1]) : SuccessNotification(obj[0].returnMessage.split('|')[1]);
            setTimeout(function () {
                $('.loader').hide();
            }, 1400);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function ResizeSchedule(_event, _date, _wholeseries) {
    var _url = 'RosterBox.aspx/ResizeSchedule';
    var employeeroster = new Object();
 
    if (_wholeseries == true) {
        employeeroster.filterType = "WholeSeries";
        employeeroster.seriesID = _event.seriesID;;
    }

    employeeroster.id = $('#mainID').val();
    employeeroster.rosterID = _event.rosterID;
    employeeroster.teamID = _event.teamID;
    employeeroster.projectID = _event.projectID;
    employeeroster.endTime = _date;
    employeeroster.startTime = _event.start.format('DD-MMM-YYYY HH:mm');
    employeeroster.displayFilterType = $('#selectViewBy').val();
    employeeroster.employeeID = _event.employeeID;
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ 'roster': employeeroster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {       
            var obj = JSON.parse(response.d);
            if (obj == "SessionTimeout")
                window.location.replace("../SessionExpire.aspx");

            if (obj.length <= 0) {
                _resources = _emptyResources;
                $('#calendar').fullCalendar('refetchResources');
                $('.loader').hide();
                return true;
            }

            var objevent = JSON.parse(response.d);
            _resources = obj;
            _resources.push(_emptyResources);
            $('#calendar').fullCalendar('refetchResources');
            _events = [];
            _events = objevent;
            $('#calendar').fullCalendar('gotoDate', objevent[0].start);
            $.each(_events.map((o) => o.title), function (key, val) {
                _events[key].title = _events[key].eventTitle;
            });


            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');
            setTimeout(function () {
                $('.loader').hide();
            }, 1400);


            var _messageType = obj[0].returnMessage.split('|')[0] === "Warning" ? WarningNotification(obj[0].returnMessage.split('|')[1]) : SuccessNotification(obj[0].returnMessage.split('|')[1]);

        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function loadNewSchedule(_id, _date, _eventStatus,_startTime) {
    // $("#formNewSchedule").show();
    $(".whole-series").hide();
   $("#btnmodal").click();
   $('#mystate').val("New");
   $('#selectTeamProject').val("");
   $('#title').val("");
   $('#rosterDate').val("");
   $('#startTime').val("09:00:00");
   $('#endTime').val("18:00:00");
   $('#description').val("");
   $('#Location').val("");
   $('#selectTeamProject').html("");
   $('#selectProject').val("");
   $('#all_day').prop('checked', false);
   $($('input[name=Team_Employee]')[0]).attr("checked", false);
   $($('input[name=Team_Employee]')[1]).attr("checked", false);
   $('input[name=updatewholeseries]').attr("checked", false);
   $($('input[name=Team_Employee]')[0]).removeAttr("disabled");
   $($('input[name=Team_Employee]')[1]).removeAttr("disabled");
   $('#selectTeamProject').removeAttr('disabled');

   $('.editOption').html("");
   if ($('#selectViewBy').val() === "Project") {
       $('#selectProject').val($('#selectViewByList').val());
   }
   else if ($('#selectViewBy').val() === "Team") {
       $($('input[name=Team_Employee]')[0]).prop("checked", true);
       GetTeamProject("Team");
       $('#selectTeamProject').val($('#selectViewByList').val());
       $('#selectTeamProject').change();
   }

   else if ($('#selectViewBy').val() === "Employee") {
       $($('input[name=Team_Employee]')[1]).prop("checked", true);
       GetTeamProject("Employee");
       $('#selectTeamProject').val($('#selectViewByList').val());
       $('#selectTeamProject').change();
   }
   GetCommonSettings();
}

function GetEventByID(_event) {
    //start.format('HH:mm')
    //startTime.value="15:00"
    // $("#formNewSchedule").show();
    $(".whole-series").show();
    $("#btnmodal").click();
    var _url = "";
    _url = 'RosterBox.aspx/GetEventByID';
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
            if (obj == "SessionTimeout")
                window.location.replace("../SessionExpire.aspx");
            $('#mystate').val("Old");
            $('#mainID').val(_event.id);
            $('#rosterID').val(_event.rosterID)
            $($('input[name=Team_Employee]')[0]).prop("checked", true);
            $($('input[name=Team_Employee]')[0]).prop("disabled", true);
            $($('input[name=Team_Employee]')[1]).prop("disabled", true);
            $('input[name=updatewholeseries]').attr("checked", false);
            $('#hdnSeriesID').val(obj.seriesID);

            $('#selectTeamProject').attr('disabled',true);

            //if ($('#selectViewBy').val() === "Project") {
            //    $('#selectTeamProject').val(obj.teamID);
            //}
            //else {
            //    $('#selectTeamProject').val(obj.projectID);
            //}

            $('#selectProject').val(obj.projectID);
            $('.editOption').html("").hide();
            GetTeamProject("Team");
            $('#selectTeamProject').val(obj.teamID);
            if (obj.allday === 'true')
                $('#all_day').prop('checked', true);
            else
                $('#all_day').prop('checked', false);

           $('#title').val(obj.title);
           $('#rosterDate').val(obj.rosterDate);


           //$('#rosterDate').attr("value", obj.rosterDate);
           //$('#rosterDateTo').attr("value", obj.rosterDateTo);

           //var _todatepicker = $('input[id="rosterDateTo"]').daterangepicker({
           //    singleDatePicker: true,
           //    showDropdowns: true,
           //    locale: {
           //        format: 'DD-MMM-YYYY'
           //    }
           //});
 
           $('#rosterDateTo').val(obj.rosterDateTo);
           $('#hdnRosterDateFrom').val(obj.rosterDate);
           $('#hdnRosterDateTo').val(obj.rosterDateTo);
           $('#startTime').val(obj.start);
           $('#endTime').val(obj.end);
           $('#description').val(obj.description);
           $('#breaknedtday').attr('checked', obj.BRKNEXTDAY);
           $('#shiftEnd').val(obj.shiftEnd);
           $('#shiftEndTime').val(obj.shiftEndTime);
           $('#breakTime_hrs').val(obj.breakTime_hrs);
           $('#breakTime_start').val(obj.breakTime_start);
           $('#early_in').val(obj.early_in);
           $('#late_in').val(obj.late_in);

           $('#early_out').val(0);
           $('#breakTime_OT_hrs').val(obj.breakTime_OT_hrs);
           $('#breakTime_OT_start').val(obj.breakTime_OT_start);

           initDatePicker();
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function initDatePicker() {
    $('#rosterDate').removeAttr("value");
    $('#rosterDateTo').removeAttr("value");
    $('#rosterDate').attr("value", $('#rosterDate').val());
    $('#rosterDateTo').attr("value", $('#rosterDateTo').val());

    $('input[id="rosterDate"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'DD-MMM-YYYY'
        }
    });

    $('input[id="rosterDateTo"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'DD-MMM-YYYY'
        }
    });

    $('input[id="shiftEnd"]').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        locale: {
            format: 'DD-MMM-YYYY'
        }
    });
}

function loadchedulse() {
    $.getJSON('RosterBox.aspx/NewEmployeeRoster', function (locationsArray) {
        _events = locationsArray._list2;
    });
}

function setEventColorCodes(_eventStatus, element) {
    //if (_eventStatus === 'Postponed' || _eventStatus === 'Rejected')
    //    element.css('background-color', '#ff0000');
    //else if (_eventStatus === 'Preponed')
    //    element.css('background-color', '#FF4500');
    //else if (_eventStatus === 'Accepted')
    //    element.css('background-color', '#1bbc9b');
    //else if (_eventStatus === 'Completed')
    //    element.css('background-color', '#008000');

    //else if (_eventStatus === 'Approved')
    //    element.css('background-color', '#1bbc9b')
    //else 
        element.css('background-color', '#95a5a6');
}

function setLabelColorCodes(_eventStatus) {
    if (_eventStatus === 'Postponed' || _eventStatus === 'Rejected')
        $('.jsStatus').css('color', '#ff0000')
    else if (_eventStatus === 'Preponed')
        $('.jsStatus').css('color', '#FF4500')
    else if (_eventStatus === 'Accepted')
        $('.jsStatus').css('color', '#1bbc9b')
    else if (_eventStatus === 'Completed')
        $('.jsStatus').css('color', '#008000')
    else if (_eventStatus === 'Approved')
        $('.jsStatus').css('color', '#1bbc9b')
     else 
        $('.jsStatus').css('color', '#95a5a6')
}

function operateTime(_time) {
    var time = $("#StartTime2").val();
    if (_time != undefined)
        time = _time;

    var hours = Number(time.match(/^(\d+)/)[1]);
    var minutes = Number(time.match(/:(\d+)/)[1]);
    var AMPM = "AM"; //time.match(/\s(.*)$/)[1];
    //if (AMPM == "PM" && hours < 12) hours = hours + 12;
    if (AMPM == "AM" && hours == 12) hours = hours - 12;
    var sHours = hours.toString();
    var sMinutes = minutes.toString();
    if (hours < 10) sHours = "0" + sHours;
    if (minutes < 10) sMinutes = "0" + sMinutes;
    //alert(sHours + ":" + sMinutes);
    $("#StartTime").val(sHours + ":" + sMinutes);
}

function saveNewRoster(_id, _date, _eventStatus) {
    var _url = 'RosterBox.aspx/SaveNewRoseter'; 

    var employeeroster = new Object();

    employeeroster.rosterDate = $('#rosterDate').val();
    employeeroster.startTime = employeeroster.rosterDate.split(" ")[0] + " " + $('#startTime').val();
    //employeeroster.endTime = employeeroster.rosterDate.split(" ")[0] + " " + $('#endTime').val();
    //employeeroster.endTime = $('#rosterDateTo').val().split(" ")[0] + " " + $('#endTime').val();//New added line
 

    
    //employeeroster.endTime = $('#endTime').val();
    employeeroster.endTime = $('#rosterDateTo').val() + " " + $('#endTime').val();
    employeeroster.employeeIds = new Array();
    var needempcode = true;
    var _assignedto = $('input[name=Team_Employee]:checked').val();
    if (_assignedto === "Employee" || _assignedto === "Team")
        $.each($('.editOption').find('.span-emp[data-val]'), function (index, val) {
            var employeeView = new Object();
            employeeView.emp_code = $(val).data('val');
            employeeView.filterType = $(val).data('type');
                  
            employeeroster.employeeIds.push(employeeView);
            needempcode = false;
           
        })
      


    if (ValidateFieldsBeforeSaving($('#mystate').val()) == false)
        return false;
    if (validateTime() == false) {
        WarningNotification("End time cannot be less than or equal to start time in case of single day.");
        return false;
    }

  
    employeeroster.filterType = "";
    if ($('#mystate').val() === "Old") {
        employeeroster.id = $('#mainID').val();
        employeeroster.rosterID = $('#rosterID').val();
        _url = 'RosterBox.aspx/UpdateEmployeeRoster';

        if ($('#updatewholeseries').is(':checked')) {
            employeeroster.filterType = "WholeSeries";
            employeeroster.seriesID = $('#hdnSeriesID').val();
        }
        
        if (needempcode == true && $('#selectViewBy').val() === 'Employee')
            employeeroster.employeeID = $('#selectViewByList').val();

           
   
        //employeeroster.startTime = $('#hdnstartTime').val();
       // if ($('#hdnendTime').val() == "")//todo
           // employeeroster.endTime = employeeroster.startTime.split(" ")[0] + " " + employeeroster.endTime;//todo
        //else Todo
           // employeeroster.endTime = $('#hdnendTime').val().split(" ")[0] + " " + employeeroster.endTime;
  
    }



    employeeroster.projectID = $('#selectProject').val();
    if (_assignedto === "Team")
        employeeroster.teamID = $('#selectTeamProject').val();
    else if (_assignedto === "Employee")
        employeeroster.filterType = "Employee";
    else
        employeeroster.teamID = 0;

    employeeroster.displayFilterType = $('#selectViewBy').val();

    employeeroster.rosterType = "Fixed";
    employeeroster.description = $('#description').val();
    employeeroster.title = $('#title').val()
    employeeroster.allday = $('#all_day').is(':checked');


    //Roster settings fields
    employeeroster.shiftEnd = $('#shiftEnd').val() + " " + $('#shiftEndTime').val();
    employeeroster.company_id = $('#company_id').val();
    employeeroster.early_in = $('#early_in').val();
    employeeroster.late_in = $('#late_in').val();
    employeeroster.early_out = $('#early_out').val();
    employeeroster.breakTime_start = $('#breakTime_start').val();
    employeeroster.breakTime_hrs = $('#breakTime_hrs').val();
    employeeroster.breakTime_OT_start = $('#breakTime_OT_start').val();
    employeeroster.breakTime_OT_hrs = $('#breakTime_OT_hrs').val();
    employeeroster.BRKNEXTDAY = $('#breaknedtday').is(':checked');

    employeeroster.BreakTimeOT = $('#BreakTimeOT').val();
employeeroster.BreakTimeOT = $('#BreakTimeOT').val();
    //Roster settings fields
    
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ 'roster': employeeroster }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);

            if (obj == "SessionTimeout")
                window.location.replace("../SessionExpire.aspx");

            if (obj.length <= 0) {
                _resources = _emptyResources;
                $('#calendar').fullCalendar('refetchResources');
                $('.loader').hide();
                return true;
            }

            var objevent = JSON.parse(response.d);
            _resources = obj;
            _resources.push(_emptyResources);
           // _resources = _resources + _emptyResources;
            $('#calendar').fullCalendar('refetchResources');
            _events = [];
            _events = objevent;
            var _message = "";

            $('#calendar').fullCalendar('gotoDate', objevent[0].start);
            $.each(_events.map((o) => o.title), function (key, val) {
                _events[key].title = _events[key].eventTitle;
              });

            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');

            var _messageType = obj[0].returnMessage.split('|')[0];
            if (_messageType != "Warning")
                $('#btnCancelModal').click();
             _messageType = obj[0].returnMessage.split('|')[0] === "Warning" ? WarningNotification(obj[0].returnMessage.split('|')[1]) : SuccessNotification(obj[0].returnMessage.split('|')[1]);
            setTimeout(function () {
                $('.loader').hide();
            }, 1400);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function ValidateFieldsBeforeSaving(_rosterSaveType) {
    var _return = true;

    if (_rosterSaveType == "beforeSave") {
        
        if ($('#selectViewBy').val() === "View by") {
            WarningNotification("Select project,team or employee.");
            return false;
        }

        
       else if ($('#selectViewByList').val() === "") {
            WarningNotification("Select project,team or employee.");
            return false;
        }
        return _return;

    }
   
    
    if ($.trim($('#rosterDate').val()) === "") {
        WarningNotification("Start date is empty.");
        return false;
    }

    else if ($.trim($('#rosterDateTo').val()) === "") {
        WarningNotification("End date is empty.");
        return false;
    }

    else if ($('#startTime').val() === "") {
        WarningNotification("start time cannot be empty.", "formNewSchedule2");
        return false;
    }
    else if ($('#endTime').val() === "") {
        WarningNotification("End time cannot be empty.", "formNewSchedule2");
        return false;
    }

    else if ($.trim($('#title').val()) === "") {
        WarningNotification("Title cannot be empty.");
        return false;
    }
    else if ($.trim($('#selectProject').val()) === "") {
        WarningNotification("Project cannot be empty.");
        return false;
    }

    if (_rosterSaveType === "New") {
        if ($('.editOption').find('.span-emp[data-val]').length < 1) {
            WarningNotification("Select the team or employee for roster.", "formNewSchedule2");
            return false;
        }
    }

    return _return;
}

function validateTime() {
    var _return = true;
    var _startdate = new Date($('#rosterDate').val() + " " + $('#startTime').val());
    var _enddate = new Date($('#rosterDateTo').val() + " " + $('#endTime').val());
    var diff = _enddate.getTime() - _startdate.getTime();
    //alert(diff / (1000*60*60*24));    
    var date = new Date(diff);
    var _days = _enddate.getDay() - _startdate.getDay();

    //var _tmestart = _startdate.getHours();
    //var _timestop = _enddate.getHours();

    var _tmestart = _startdate.getTime();
    var _timestop = _enddate.getTime();
    var hourDiff = _timestop - _tmestart;
    if (_days <= 0 && hourDiff <= 0)
        _return = false;

    return _return;
}

function GetProjects(_viewBy,_callfrom) {
    //$("#formNewSchedule").show(); 
    var _url = 'RosterBox.aspx/GetProjects';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_viewBy': _viewBy }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var _data = JSON.parse(data.d);
            //_events = [];
            //_events.push(obj);

            var procemessage = "<option value=''> </option>";
           
            var markup = "<option value=''>--select-- </option>";
            //var _data = _allProducts != "" ? data._sortedList : data._sortedList2;
            for (var x = 0; x < _data.length; x++) {
                markup += "<option value=" + _data[x].mainID + ">" + _data[x].Description + "</option>";
            }

            if (_callfrom === "Project") {
                $("#selectProject").html(procemessage).show();
                $("#selectProject").html(markup).show();
            }
            else {
                $("#selectViewByList").html(procemessage).show();
                $("#selectViewByList").html(markup).show();
                //$("#selectViewByList").val($('#hdnID').val());
            }


        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}


function GetTeamProject(_viewBy, _callfrom) {
    //$("#formNewSchedule").show(); 
    var _url = 'RosterBox.aspx/GetProjects';
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_viewBy': _viewBy }), // Check this call.
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var _data = JSON.parse(data.d);
            //_events = [];
            //_events.push(obj);

            var procemessage = "<option value='0'> Please wait...</option>";
            
            var markup = "";
            if (_viewBy==="Team")
                markup = "<option value='' selected='selected'> --Choose Team--</option>";// "<option value='-1'></option";
            else
                markup = "<option value='' selected='selected'> --Choose Employee--</option>";
            //var _data = _allProducts != "" ? data._sortedList : data._sortedList2;
            for (var x = 0; x < _data.length; x++) {
                markup+="<option value=" + _data[x].mainID + ">" + _data[x].Description + "</option>";
            }
            //$("#selectTeamProject").html(procemessage).show();
            $("#selectTeamProject").html(markup).show();
            //$("#selectTeamProject").prepend("<option value='' selected='selected'></option>");

           // $('#test').html(markup);
            //$("#selectViewByList").val($('#hdnID').val());



        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}

_resources = [
    { id: '', title: '', occupancy: 40 }
    //{ id: '1', title: 'Jaspreet', occupancy: 40 },
    //{ id: '2', title: 'Abhishek', occupancy: 40, eventColor: 'green' },
    //{ id: '3', title: 'Sachin', occupancy: 40, eventColor: 'orange' },
    //{ id: '4', title: 'Harpal', occupancy: 40 }
];

_emptyResources = [
    { id: '', title: '', occupancy: 40 }
];


function GetEmployeeRosterList() {
    var _url = 'RosterBox.aspx/GetEmployeeRosterList';

    var employeeroster = new Object();
    var _viewby = "";
    if ($('#selectViewBy').val() === "Project") {
        employeeroster.displayFilterType = "Project";
        employeeroster.projectID = $('#selectViewByList').val();
    }
   else if ($('#selectViewBy').val() === "Employee") {
       employeeroster.displayFilterType = "Employee";
        employeeroster.employeeID = $('#selectViewByList').val();
    }
    else {
       employeeroster.displayFilterType = "Team";
        employeeroster.teamID = $('#selectViewByList').val();
    }


    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ 'roster': employeeroster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend:function(){
            $('.loader').show();
        },
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj == "SessionTimeout")
                window.location.replace("../SessionExpire.aspx");
            if (obj.length <= 0) {
                _resources = _emptyResources;
                $('#calendar').fullCalendar('refetchResources');
                $('.loader').hide();
                return true;
            }

            var objevent = JSON.parse(response.d);
            _resources = obj;
            _resources.push(_emptyResources);
            $('#calendar').fullCalendar('refetchResources');
            _events = [];
            _events = objevent;
            $('#calendar').fullCalendar('gotoDate', objevent[0].start);
            $.each(_events.map((o) => o.title), function (key, val) {
                _events[key].title = _events[key].eventTitle;
            });


            $("#calendar").fullCalendar('removeEvents');
            $("#calendar").fullCalendar('addEventSource', _events);
            $("#calendar").fullCalendar('rerenderEvents');
            //$('#calendar').fullCalendar('renderEvents', _events, 'stick');

            
            //$('#calendar').fullCalendar('addEvent', _events)
            // $('#calendar').fullCalendar('unselect');

            setTimeout(function () {
                $('.loader').hide();
            }, 1400);
          
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function DeleteEvent(_id, _date, _eventStatus) {
    var _url = 'RosterBox.aspx/DeleteEvent';
    var employeeroster = new Object();

    //if (_wholeseries == true) {
    //    employeeroster.filterType = "WholeSeries";
    //    employeeroster.seriesID = _event.seriesID;;
    //}
    employeeroster.id = $('#mainID').val();
    employeeroster.rosterID = $('#rosterID').val();
    //employeeroster.teamID = _event.teamID;
    employeeroster.projectID = $('#selectProject').val();
    employeeroster.displayFilterType = 'Project'; //$('#selectViewBy').val();

    if ($('#updatewholeseries').is(':checked')) {
        employeeroster.filterType = "WholeSeries";
        employeeroster.seriesID = $('#hdnSeriesID').val();
    }


    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_roster': employeeroster }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var obj = JSON.parse(response.d);
            if (obj == "SessionTimeout")
                window.location.replace("../SessionExpire.aspx");
            $('#selectViewByList').change();
            setTimeout(function () {
                SuccessNotification("Event deleted successfully.");
            }, 800);
           
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
   ['<div class="form-inline" id="viewbyFilters">',
                                //'<label class="col-md-1 control-label label-viewby">View by</label>',
                                '<div class="form-group">',
                                    '<select id="selectViewBy" class="form-control input-sm">',
                                        '<option value="">View by</option><option value="Project">Project</option><option value="Team">Team</option><option value="Employee">Employee</option></select>',
                                        //'<option value="">View by</option><option value="Project">Project</option></select>',
                                '</div>',
                                '<div class="form-group">',
                                   '<select id="selectViewByList" class="form-control input-sm hidden"><option value="">--select--</option></select>',
                                '</div>',
                            '</div>'
   ].join('\n');

    $('.fc-header-toolbar .fc-left *:first').before(_filtersection);
    //$('.fc-header-toolbar .fc-left').append(_filtersection);
}

function GetCommonSettings() {
    var _url = "";
    _url = 'RosterBox.aspx/GetCommonSettings';
    var roster = new Object();
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        //data: JSON.stringify({ '_id': _event.id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (_data) {
            var obj = JSON.parse(_data.d);
            $('#company_id').val(obj.company_id);
           $('#breakTime_hrs').val(obj.breakTime_hrs);
           $('#breakTime_start').val(obj.breakTime_start);
           $('#early_in').val(obj.early_in);
           $('#late_in').val(obj.late_in);
           $('#early_out').val(obj.early_out);
           $('#breakTime_OT_start').val(obj.breakTime_OT_start);
           $('#breakTime_OT_hrs').val(obj.breakTime_OT_hrs);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}