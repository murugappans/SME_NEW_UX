var _events = new Array();
var _resources = [];
$(document).ready(function () {
    //$('#modelDateLabel').text(' '+ moment().format('D MMM YYYY'));
   
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


    //////////////////////////////////////////////////////////////////
    $(document).on('change', '#selectViewBy',function(){
    //$('#selectViewBy').change(function () {
        var _selected = $(this).val();
        if (_selected === "")
            $('#spanfilters').addClass('hidden');
        else
            $('#spanfilters').removeClass('hidden');
        GetProjects(_selected, 'ViewBy');
        return true;

        if (_selected === "Project") {
            _resources = [];

            _resources = [
        { id: '', title: '', occupancy: 40 },
            ]



            //          _resources = [
            //        { id: '1', title: 'Jaspreet', occupancy: 40 },
            //{ id: '2', title: 'Abhishek', occupancy: 40, eventColor: 'green' },
            //{ id: '3', title: 'Sachin', occupancy: 40, eventColor: 'orange' },
            //{ id: '4', title: 'Harpal', occupancy: 40 }
            //          ]

            //LoadRosters();
            $('#calendar').fullCalendar('refetchEvents');
            //t.someParam = !t.someParam;
            $('#calendar').fullCalendar('refetchResources');
        }
    });

    $(document).on('change', '#selectViewByList', function () {
        return true;
        var _selected = $('#selectViewBy').val();   
           $('.loader').show();
            setTimeout(function () {
            GetEmployeeRosterList();
        }, 800);
        
    });


    $(document).on('change', '#StartTime2', function () {
        //var _fullval = $(this).val().split(' ')[0];
        //var _hours = _fullval.split(':')[0];
        //var _minutes = _fullval.split(':')[1];
        //$('#_EndTime').val(_hours + 1 + ': ' + _minutes)
        operateTime();
    });
   
    $(document).on('click', '.show-employee-roster', function (e) {
        var _elem = $(e.target);

        var _serid = _elem.data('seriesid');
        if (_elem.hasClass('badge-danger'))
            _serid = _elem.closest('a').data('seriesid');
        var _empcount =parseInt( $(this).find('.badge-danger').text());

        if (_empcount == 1) {
            var _selected = $('#selectViewBy').val();
            if (_selected === "Employee") {
                var _empid = $('#selectViewByList').val();
                GetEmployeeRosterListBySeries(_serid, _empid);
            }
            else {
                GetEmployeeRosterListBySeries(_serid, 1);
            }
        }
        else
            GetEmployeeRosterListBySeries(_serid, 0);
       

    });

    $(document).on('change', '#rosterDate,#rosterDateTo', function () {
        var _fromDate = $('#rosterDate').val();
        var _toDate = $('#rosterDateTo').val();
        _fromDate = Date.parse(_fromDate);
        _toDate = Date.parse(_toDate);
        if (_toDate < _fromDate) {
            WarningNotification("To date should be equal or greater than from date.");
            //if ($('#mystate').val() === "Old")
            //    $('#rosterDateTo').val($('#hdnRosterDateTo').val());
            //else
            //    $('#rosterDateTo').val($('#rosterDate').val());
            return false;
        }
        $('#shiftEnd').val($('#rosterDateTo').val());
        $('#shiftEndTime').val($('#endTime').val());
    });



    $(document).on('click', '#btnGo', function () {
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

        GetEmployeeRosterList();

    });
});

function GetEmployeeRosterListBySeries(_serid,empid) {
    var _url = 'EmployeeRosterList.aspx/GetEmployeeRosterListBySeries';

    var employeeroster = new Object();
    var _viewby = "";
   
    $.ajax({
        type: "POST",
        url: _url,
        async: false,
        data: JSON.stringify({ '_serid': _serid,'_empID':empid }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
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

           // $('#InputWorkerAttendance tbody').text("");


            $('.roster-more-detail').text("");

            var _ribbon =
[

                         '<div id="divemployees" class="mt-element-ribbon bg-grey-steel">',
                                 '<div class="ribbon ribbon-border-hor ribbon-clip ribbon-color-danger uppercase">',
                                     '<div></div> Employees </div>',
                               
                                 //'<p class="ribbon-content">' + val.employeeName + '</p>',
                             '</div>',
].join('\n');
            if ($('#divemployees').length == 0)
                $('.roster-more-detail').append(_ribbon);
            $.each(objevent, function (key, val) {
                //    var _tr =
                //['<tr>',
                //'<td>' + val.employeeName + '</td>',
                //'<td>' + val.projectName + '</td>',
                //'<td>' + val.teamName + '</td>',
                //'<td>' + val.start.split('|')[0] + '</td>',
                //'<td>' + val.end.split('|')[0] + '</td>',
                //'<td>' + val.start.split('|')[1] + '</td>',
                //  '<td>' + val.end.split('|')[1] + '</td>',
                //'<td>' + val.start_end + '</td>',
                //'<td>' + val.title + '</td>',
                //'<td>' + val.description + '</td>',
                //'</tr>'
                //].join('\n');
                //$('#InputWorkerAttendance tbody').append(_tr);
                //$('#employeeRosterList').preappend( $('#InputWorkerAttendance'));

        

                var _p = '<p class="ribbon-content">' + val.employeeName + '</p>';
                        
                $('#divemployees').append(_p);
            });
                //$('.roster-more-detail').append(_ribbon);
            setTimeout(function () {
                $('.loader').hide();
            }, 1400);

        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function loadNewSchedule(_id, _date, _eventStatus) {
    // $("#formNewSchedule").show();
   $("#btnmodal").click();
   $('#mystate').val("New");
   $('#selectTeamProject').val("");
   $('#title').val("");
   $('#rosterDate').val("");
   $('#startTime').val("");
   $('#endTime').val("");
   $('#description').val("");
   if ($('#selectViewBy').val() === "Project")
       $('#labelTeamProject').text("Team");
   else
       $('#labelTeamProject').text("Project");
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

            if (_data.length <= 0) {
                $("#selectViewByList").html("");
                $('.loader').hide();
                return true;
            }

            var procemessage = "<option value='0'> Please wait...</option>";
            $("#selectViewByList").html(procemessage).show();
            var markup = "";
            //var _data = _allProducts != "" ? data._sortedList : data._sortedList2;
            for (var x = 0; x < _data.length; x++) {
                markup += "<option value=" + _data[x].mainID + ">" + _data[x].Description + "</option>";
            }

            //if (_callfrom === "ViewBy")
            //    $("#selectViewByList").html(markup).show();
            //else
            $("#selectViewByList").html(markup).show();
            //$("#selectViewByList").val($('#hdnID').val());



        },
        failure: function (response) {
            alert(response.d);
        }
    });
    return true;
}

function GetEmployeeRosterList() {
    //var _url = 'RosterBox.aspx/GetRoseterList';
    var _url = 'EmployeeRosterList.aspx/GetEmployeeRosterListByGrouping';

    var employeeroster = new Object();
    var _viewby = "";
    if ($('#selectViewBy').val() === "Project") {
        employeeroster.filterType = "Project";
        employeeroster.projectID = $('#selectViewByList').val();
    }
    else if ($('#selectViewBy').val() === "Employee") {
        employeeroster.filterType = "Employee";
        employeeroster.employeeID = $('#selectViewByList').val();
    }
    else if ($('#selectViewBy').val() === "Team") {
        employeeroster.filterType = "Team";
        employeeroster.teamID = $('#selectViewByList').val();
    }
    else {
        WarningNotification("One of your filter is either incomplete or invalid.");
        return true;
    }

    employeeroster.startTime = $('#rosterDate').val();
    employeeroster.endTime = $('#rosterDateTo').val();
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
            $('#divemployees').html("").hide();
            var obj = JSON.parse(response.d);
            if (obj.length <= 0) {
                $('#InputWorkerAttendance tbody').text("No record found.");
                $('#employeeRosterList').text("");
                $('.loader').hide();
                WarningNotification("No record found for set filters.");
                return true;
            }

            var objevent = JSON.parse(response.d);

            $('#InputWorkerAttendance tbody').text("");
            $('#employeeRosterList').text("");
            var _strDates = "";
            $.each(objevent, function (key, val) {
            //    var _tr =
            //['<tr>',
            //'<td>' + val.employeeName + '</td>',
            //'<td>' + val.projectName + '</td>',
            //'<td>' + val.teamName + '</td>',
            //'<td>' + val.start.split('|')[0] + '</td>',
            //'<td>' + val.end.split('|')[0] + '</td>',
            //'<td>' + val.start.split('|')[1] + '</td>',
            //  '<td>' + val.end.split('|')[1] + '</td>',
            //'<td>' + val.start_end + '</td>',
            //'<td>' + val.title + '</td>',
            //'<td>' + val.description + '</td>',
            //'</tr>'
            //].join('\n');
            //$('#InputWorkerAttendance tbody').append(_tr);
                //var _timeend = getTwelvehoursTime(val.timestart);
                _strDates += val.start.split(' | ')[0] + " ";
                var _date = val.start.split('|')[0];
                var _classdate = _date.split(' ')[0] + '-' + _date.split(' ')[1] + '-' + _date.split(' ')[2];
                var _time = "";
                var _enddate = val.end.split('|')[0];
                if (_date === _enddate)
                    _enddate = "";
                else
                    _enddate = '    till ' + val.end.split('|')[0];
                if ($('.' + _classdate).length == 0) {
                     _time = [
                            '<time  datetime=' + val.start.split(' | ')[0] + '>',
                            '<span class="month">' + val.start.split(' | ')[0].split(" ")[1] + '</span>',
                            '<span class="day">' + val.start.split(' | ')[0].split(" ")[0] + '</span>',                               
                               //'<span class="year">' + val.start.split('|')[0] + '</span>', show year
                               //'<span class="time">12:00 AM</span>',
                           '</time>',
                            '<div class="info ' + _classdate + '">',
                            '<p class="title text-left">' + val.title + " | " + val.projectName + " | " + getTwelvehoursTime(val.timestart) + " to " + getTwelvehoursTime(val.timeend) + " | hours " + val.start_end + _enddate +'<a class="show-employee-roster" data-seriesid=' + val.seriesID  + '><span class="badge badge-danger">' + val.color + '</span>   Employees</a></p>',
                            //'<p class="desc text-left">Number of employees: ' + val.color + '        ' + '<a class="show-employee-roster" data-seriesid=' + val.seriesID + '>Show detail</a></p>',
                           // '<a class="show-employee-roster">Show detail</a>',
                           '</div>',
                   ].join('\n');
                }
                else {
                    var _info = [
                           //'<div class="info-cust">',
                            '<p class="title text-left">' + val.title + " | " + val.projectName + " | " + getTwelvehoursTime(val.timestart) + " to " + getTwelvehoursTime(val.timeend) + " |  hours " + val.start_end + _enddate + '<a class="show-employee-roster" data-seriesid=' + val.seriesID  + '><span class="badge badge-danger">' + val.color + '</span>   Employees</a></p>',
                           //'<h4 class="title text-left">' + val.title + '</h4>',
                            //'<p class="desc text-left">Number of employees: ' + val.color + '        ' + '<a class="show-employee-roster" data-seriesid=' + val.seriesID + '>Show detail</a></p>',
                          // '</div>',
                    ].join('\n');

                    $('.' + _classdate).append(_info);
                }



   
                var _li =
               [
                '<li>',
                       //'<time datetime=' + val.start.split(' | ')[0] + '>',
                       //     '<span class="day">' + val.start.split(' | ')[0].split(" ")[0] + '</span>',
                       //        '<span class="month">' + val.start.split(' | ')[0].split(" ")[1] + '</span>',
                       //        '<span class="year ' + _classdate + '">' + val.start.split('|')[0] + '</span>',
                       //        //'<span class="time">12:00 AM</span>',
                       //    '</time>',
                      _time,

                           //'<div class="info">',
                           //'<h2 class="title text-left">' + val.title + '</h2>',
                           // '<p class="desc text-left">Number of employees: ' + val.color + '        '+'<a class="show-employee-roster" data-seriesid='+ val.seriesID +'>Show detail</a></p>',
                           //// '<a class="show-employee-roster">Show detail</a>',
                           //'</div>',
              '</li>'
               ].join('\n');
                if ($.trim($(_li).text()).length > 0)
                $('#employeeRosterList').append(_li);
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

function getTwelvehoursTime(_time) {
   var time = _time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [_time];

    if (time.length > 1) { // If time format correct
        time = time.slice(1);  // Remove full string match value
        time[5] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
        time[0] = +time[0] % 12 || 12; // Adjust hours
    }
    time.splice(3, 1);
    return time.join(''); // return adjusted time or original string

}