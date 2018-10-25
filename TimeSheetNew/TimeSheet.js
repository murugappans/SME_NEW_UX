




Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}



if (!Date.prototype.toISOString) {
    (function () {

        function pad(number) {
            if (number < 10) {
                return '0' + number;
            }
            return number;
        }

        Date.prototype.toISOString = function () {
            return this.getUTCFullYear() +
                '-' + pad(this.getUTCMonth() + 1) +
                '-' + pad(this.getUTCDate()) +
                'T' + pad(this.getUTCHours()) +
                ':' + pad(this.getUTCMinutes()) +
                ':' + pad(this.getUTCSeconds()) +
                '.' + (this.getUTCMilliseconds() / 1000).toFixed(3).slice(2, 5) +
                'Z';
        };

    }());
}


angular.module('TimesheetApp', ['toaster', 'ui.bootstrap', 'png.timeinput', 'ngFileUpload'])

    .filter('truncate', function () {
        return function (value, wordwise, max, tail) {
            if (!value) return '';
            max = parseInt(max, 10);
            if (!max) return value;
            if (value.length <= max) return value;

            value = value.substr(0, max);
            if (wordwise) {
                var lastspace = value.lastIndexOf(' ');
                if (lastspace != -1) {
                    value = value.substr(0, lastspace);
                }
            }
          //  return value + (tail || ' …');
            return value;
        };
    })
    .directive('dotLoading', function () {
        return {
            restrict: 'E',
            replace: true,

            scope: {
                loading: "="
            },
            template: '<div class="loading" > Loading &#8230;</div >',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    return scope.loading;
                };

                scope.$watch(scope.isLoading, function (v) {
                    console.log(v)
                    if (v) {
                        elm.show();
                    } else {
                        elm.hide();
                    }
                });
            }
        };
    })

    .directive("compareTo", function () {
        return {
            require: "ngModel",
            scope: {
                confirmPassword: "=compareTo"
            },
            link: function (scope, element, attributes, modelVal) {

                modelVal.$validators.compareTo = function (val) {
                    console.log(val);
                    return false;
                };

                scope.$watch("confirmPassword", function () {
                    modelVal.$validate();
                });
            }
        };
    })

    .directive('timeMask', function ($filter) {
        return {
            restrict: 'A',
            priority: 1,
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {
                var timeFormat;

                function hours_am_pm(time) {

                    var hours = time[0] + time[1];
                    console.log(hours)
                    var min = time[2] + time[3];
                    if (hours < 12) {
                        return hours + ':' + min + ' AM';
                    } else {
                        hours = hours - 12;
                        hours = (hours.length < 10) ? '0' + hours : hours;
                        return hours + ':' + min + ' PM';
                    }
                }




                function formatter(value) {
                    console.log(timeFormat)
                    var formattedValueLength = timeFormat.length;
                    var unformattedValueLength = timeFormat.replace(':', '').length;
                    var timeMask = new String(timeFormat);

                    if (ngModel.$isEmpty(value)) {
                        console.log("emty")
                        return value;

                    }
                    if (!value) {
                        ngModel.$setValidity('time', true);
                        return null;
                    } else if (angular.isDate(value) && !isNaN(value)) {
                        ngModel.$setValidity('time', true);
                        return value;
                    }
                    else if (angular.isString(value)) {
                        console.log("is string")
                        var timeRegex = /^(0?[0-9]|1[0-2]):[0-5][0-9] ?[a|p]m$/i;
                        if (!scope.showMeridian) {
                            timeRegex = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
                        }
                        if (!timeRegex.test(value)) {
                            console.log("rex")
                            ngModel.$setValidity('time', false);
                            return undefined;
                        }
                        else {
                            console.log("split")
                            ngModel.$setValidity('time', true);
                            var date = new Date();
                            var sp = value.split(":");
                            var apm = sp[1].match(/[a|p]m/i);
                            if (apm) {
                                sp[1] = sp[1].replace(/[a|p]m/i, '');
                                if (apm[0].toLowerCase() == 'pm') {
                                    sp[0] = sp[0] + 12;
                                }
                            }
                            date.setHours(sp[0], sp[1]);
                            return date;
                        }
                    }
                    else {
                        ngModel.$setValidity('time', false);
                        return undefined;
                    }
                }

                var showTime = function (data) {
                    timeFormat = (!scope.showMeridian) ? "HH:mm" : "hh:mm a";
                    return $filter('date')(formatter(data), timeFormat);
                };

                scope.$watch('showMeridian', function (value) {
                    var myTime = ngModel.$modelValue;
                    if (myTime) {
                        element.val(showTime(myTime));
                    }

                });

                ngModel.$formatters.push(function (value) {
                    console.log("for")
                    timeFormat = (!scope.showMeridian) ? "HH:mm" : "hh:mm a";

                    var date = formatter(value);
                    return date;
                });

                ngModel.$parsers.push(function (value) {

                    console.log("par")
                    var n1 = value.split('_');

                    var date = hours_am_pm(n1[0] + n1[1]);
                    console.log(date)
                    return date;
                });


            } //link function
        };
    })

    .directive('timeX', function () {
        return {
            restrict: 'E',
            replace: true,
            template: '<Input type="text" ng-model="model" />',
            scope: {
                model: '=',
                timeMode: '='
            },
            link: function (scope, element, attrs, ngModelController) {


                element.on('blur', function (data) {

                    var value = scope.model;



                    if (angular.isString(value)) {
                        var n1 = value.split('_');

                        var date = hours_am_pm(n1[0] + n1[1]);


                        console.log(date);
                        scope.model = date;
                    }

                });





            }




        }

        function blurCall() {
            var value = scope.model;



            if (angular.isString(value)) {
                var n1 = value.split('_');

                var date = hours_am_pm(n1[0] + n1[1]);


                console.log(date);
                scope.model = date;
            }

        }


        function hours_am_pm(time) {

            var hours = time[0] + time[1];
            console.log(hours)
            var min = time[2] + time[3];
            if (hours < 12) {
                return hours + ':' + min + ' AM';
            } else {
                hours = hours - 12;
                hours = (hours.length < 10) ? '0' + hours : hours;
                return hours + ':' + min + ' PM';
            }
        }

    })

    .directive("numbersOnly", function () {


        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attr, ngModelCtrl) {
                function fromUser(text) {
                    console.log(text)
                    if (text) {
                        var transformedInput = text.replace(/[^0-9-]/g, '');
                        if (transformedInput !== text) {
                            ngModelCtrl.$setViewValue(transformedInput);
                            ngModelCtrl.$render();
                        }
                        return transformedInput;
                    }
                    return '';
                }
                ngModelCtrl.$parsers.push(fromUser);
            }

        }


    })


    .directive('customPopover', function () {
        return {
            restrict: 'A',
            //  template: '<span>{{label}}</span>',
            link: function (scope, el, attrs) {
                //     scope.label = attrs.popoverLabel;
                $(el).popover({
                    trigger: 'hover',
                    html: true,
                    content: function () {
                        return attrs.popoverHtml
                    },
                    placement: attrs.popoverPlacement
                });
            }
        };
    })


    .directive('allowOnlyNumbers', function () {
        return {
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {



                elm.on('keydown', function (event) {
                    var $input = $(this);


                    var value = $input.val();

                    console.log($input.val())
                    value = value.replace(/[^0-9]/g, '')
                    $input.val(value);

                    if (event.which == 64 || event.which == 16) {
                        // to allow numbers  
                        return false;
                    } else if (event.which >= 48 && event.which <= 57) {
                        // to allow numbers  
                        return true;
                    } else if (event.which >= 96 && event.which <= 105) {
                        // to allow numpad number  
                        return true;
                    } else if ([9, 8, 13, 27, 37, 38, 39, 40].indexOf(event.which) > -1) {
                        // to allow backspace, enter, escape, arrows 
                        $input.val('');
                        return true;
                    } else {

                        event.preventDefault();
                        // to stop others  
                        //alert("Sorry Only Numbers Allowed");  
                        return false;
                    }
                });
            }
        }
    })


    .directive('strtoTime', ['dateFilter', function (dateFilter) {
        return {
            require: 'ngModel',
            scope: {
                format: "="
            },
            link: function (scope, element, attrs, ngModelController) {

                function hours_am_pm(time) {

                    var hours = time[0] + time[1];
                    console.log(hours)
                    var min = time[2] + time[3];
                    if (hours < 12) {
                        return hours + ':' + min + ' AM';
                    } else {
                        hours = hours - 12;
                        hours = (hours.length < 10) ? '0' + hours : hours;
                        return hours + ':' + min + ' PM';
                    }
                }




                ngModelController.$parsers.push(function (data) {
                    console.log(data);
                    //convert data from view format to model format
                    //  return data;
                    //  return dateFilter(data, 'dd/MM/yyyy'); //converted
                    var n1 = data.split('_');

                    var date = hours_am_pm(n1[0] + n1[1]);
                    console.log(date)
                    return "3:45 PM";
                });

                ngModelController.$formatters.push(function (data) {
                    //convert data from model format to view format
                    console.log(data);
                    return data;
                    //return dateFilter(data, 'dd/MM/yyyy'); //converted
                });
            }
        }
    }])



    .controller('ModalInstanceCtrl', ['$modalInstance', 'item', function ($modalInstance, item) {
        var $ctrl = this;
        $ctrl.item = item;

        $ctrl.ok = function () {
            console.log("ok");
            $modalInstance.close();
        };

        $ctrl.cancel = function () {
            console.log("non");
            $modalInstance.dismiss();
        };
    }])

    .filter('time', function () {

        var conversions = {
            'ss': angular.identity,
            'mm': function (value) { return value * 60; },
            'hh': function (value) { return value * 3600; }
        };

        var padding = function (value, length) {
            var zeroes = length - ('' + (value)).length,
                pad = '';
            while (zeroes-- > 0) pad += '0';
            return pad + value;
        };

        return function (value, unit, format, isPadded) {
            var totalSeconds = conversions[unit || 'ss'](value),
                hh = Math.floor(totalSeconds / 3600),
                mm = Math.floor((totalSeconds % 3600) / 60),
                ss = totalSeconds % 60;

            format = format || 'hh:mm';
            isPadded = angular.isDefined(isPadded) ? isPadded : true;
            // hh = isPadded ? padding(hh, 2) : hh;
            mm = isPadded ? padding(mm, 2) : mm;
            ss = isPadded ? padding(ss, 2) : ss;
            if (totalSeconds > 0)
                return format.replace(/hh/, hh).replace(/mm/, mm);

            return '-';
            //  return format.replace(/hh/, hh).replace(/mm/, mm).replace(/ss/, ss);
        };
    })


    .filter('formatTime', function ($filter) {
        return function (time, format) {
            var parts = time.split(':');
            var date = new Date(0, 0, 0, parts[0], parts[1], parts[2]);
            return $filter('date')(date, format || 'h:mm');
        };
    })
    .directive('loading', ['$http', function ($http) {
        return {
            restrict: 'A',
            //template: '<div class="loading-spiner"><img src="http://www.nasa.gov/multimedia/videogallery/ajax-loader.gif" /> </div>',
            template: '<div class="loading" > Loading &#8230;</div >',
            link: function (scope, elm, attrs) {
                scope.isLoading = function () {
                    return $http.pendingRequests.length > 0;
                };

                scope.$watch(scope.isLoading, function (v) {
                    if (v) {
                        elm.show();
                    } else {
                        elm.hide();
                    }
                });
            }
        };
    }])
    .directive('expand', function () {
        return {
            restrict: 'A',
            controller: ['$scope', function ($scope) {
                $scope.$on('onExpandAll', function (event, args) {
                    $scope.expanded = args.expanded;
                });
            }]
        };
    })



    .directive('formattedDate', ['dateFilter', function (dateFilter) {
        return {
            require: 'ngModel',
            scope: {
                format: "="
            },
            link: function (scope, element, attrs, ngModelController) {

                ngModelController.$parsers.push(function (data) {
                    console.log(data);
                    //convert data from view format to model format
                    //  return data;
                    return dateFilter(data, 'dd/MM/yyyy'); //converted
                });

                ngModelController.$formatters.push(function (data) {
                    //convert data from model format to view format
                    console.log(data);
                    return dateFilter(data, 'dd/MM/yyyy'); //converted
                });
            }
        }
    }])


    .directive('formattedTime', ['$filter', function ($filter) {
        return {
            require: 'ngModel',
            scope: {
                //  format: "="
            },
            link: function (scope, element, attrs, ngModelController) {

                function hours_am_pm(data) {

                    var length = data.length;

                    var n1 = data.split('_');

                    var time = n1[0] + n1[1];


                    var hours = time[0] + time[1];
                    var min = time[2] + time[3];

                    //console.log("TIME[0]" + time[0])
                    //console.log("TIME[1]" + time[1])
                    //console.log("time[2]" + time[2])
                    //console.log("time[3]" + time[3])


                    if (length == 3) {
                        //console.log(length)
                        hours = time[0];
                        min = time[1] + time[2];
                    }





                    if (length == 1) {
                        //console.log(length)
                        hours = time[0];
                        min = "00";
                    }

                    if (length == 2) {
                        //console.log(length)
                        hours = time[0] + time[1];
                        min = "00";
                    }



                    if (hours < 10 && hours.length < 2) {

                        hours = '0' + hours;
                    }




                    else if (hours > 23) {
                        hours = '00';
                    }

                    if (min < 10 && min.length < 2) {
                        min = '0' + min;
                    }
                    else if (min > 59) {
                        min = "00";
                    }

                    //if (hours < 12) {
                    //   // return hours + ':' + min + ' AM';
                    //    console.log("lessthan 12")
                    //    return hours + ':' + min;
                    //} else {
                    //    //hours = hours - 12;

                    //    console.log(">12")
                    //    hours = (hours.length < 2) ? '0' + hours : hours;
                    //    //return hours + ':' + min + ' PM';
                    //    return hours + ':' + min;
                    //}

                    return hours + ':' + min;
                }




                ngModelController.$parsers.push(function (data) {
                    //console.log(data);
                    //convert data from view format to model format
                    if (data == "--:--")
                        return "";



                    return data;
                });

                ngModelController.$formatters.push(function (data) {
                    //convert data from model format to view format
                    //  console.log(data);


                    if (data) {
                        var parts = data.split(':');

                        if (parts.length >= 2) {
                            //var date = new Date(0, 0, 0, parts[0], parts[1], parts[2]);

                            //console.log(date)
                            //  var result = $filter('date')(date, 'hh:mm');
                            var result = parts[0] + ':' + parts[1];
                        }

                        if (result == "")
                            return "--:--"

                        return result;
                    }
                    return "--:--"
                    //  return dateFilter(data, 'HH:mm'); //converted
                });
            }
        }
    }])
    .directive('ngModel', ['$filter', function ($filter) {
        return {
            require: '?ngModel',
            link: function (scope, elem, attr, ngModel) {
                if (!ngModel)
                    return;
                if (attr.type !== 'time')
                    return;

                ngModel.$formatters.unshift(function (value) {
                    return value.replace(/:\d{2}[\.\,]\d{3}$./, '')
                });
            }
        }
    }])

    .directive('tooltip', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.hover(function () {
                    // on mouseenter
                    element.tooltip('show');
                }, function () {
                    // on mouseleave
                    element.tooltip('hide');
                });
            }
        };
    })

    .controller('timesheetController', ['$http', '$scope', 'toaster', '$log', '$window', '$document', '$filter', '$modal', 'Upload',
        function ($http, $scope, toaster, $log, $window, $document, $filter, $modal,Upload) {


            var vm = this;
            vm.timesheetform;
            vm.refId = -1;;
            vm.timeMode = 24;
            vm.companyId;
            vm.loginUser = -1;
            vm.keyintime;
            vm.keyouttime;
            vm.selectedEmp = {};
            vm.dateTime;
            vm.IsSubmit = true;

            vm.IsApproved = false;

            vm.isLoading = false;

            vm.projectChangeAll = false;
            vm.nightShiftChange = false;

            vm.totalNH = 0;
            vm.totalOT1 = 0;
            vm.totalOT2 = 0;
            vm.totalLateNess = 0;
            vm.supProjectList = [];

            vm.timeSheetList = [];

            vm.modelOption = {
                headerText: 'Delete?',
                bodyText: 'Selected Claim  Entry Will be Deleted Permanently!',
                closeButtonText: 'Cancel',
                actionButtonText: 'Ok'
            };

            vm.showAlertMessages = function (messageoption) {



                var modalInstance = $modal.open({
                    templateUrl: 'myModalContent.html',
                    controller: 'ModalInstanceCtrl',
                    controllerAs: '$ctrl',
                    size: 'sm',
                    resolve: {
                        item: function () {
                            return messageoption;
                        }
                    }

                });

                return modalInstance.result;
                //modalInstance.result.then(function () {
                //    return;
                //}, function () {
                //    $log.info(' Error Modal dismissed at: ' + new Date());
                //});
            };
            vm.files;
            //mainfunction
            vm.gettimeSheetList = function () {


                if (!vm.selectedEmp.value) {
                    toaster.pop('error', "Error", " Please Select Employee");
                    return
                }





                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;

                vm.isLoading = true;

                $http({
                    method: 'GET',
                    url: '/api/TimeSheetList',
                    params: { EmpCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(function successCallback(response) {

                    console.log(response)
                    if (response.data.length == 0) {
                        toaster.pop('error', "Error", "No records found");
                    }
                    vm.getSubproject();


                    vm.timeSheetList = response.data;

                    console.log(vm.timeSheetList)

                    angular.forEach(vm.timeSheetList, function (timeSheetList) {

                        timeSheetList.isValid = true;
                        timeSheetList.ErroMsg = '';
                        //   timeSheetList.isOffDay = false;

                    });

                    vm.validateTimeSheet();


                    // todo  add dynamic properties

                    vm.GetIsTimeSheetSubmited();
                    vm.checkAll();
                    vm.UploadFileInfo();
                    vm.GetIsTimeSheetApproved();
                    addWatch();
                    calculateTotal();
               
                    vm.isLoading = false;

                },
                    function errorCallback(response) {
                        vm.isLoading = false;
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);

                    });

            };

            vm.claculate = function () {
                if (getSelectedItem().length <= 0) {
                    toaster.pop('error', "Error", "Please Select Records");
                    return;
                }
                if (vm.timesheetform.$invalid) {
                    toaster.pop('error', "Error", " Please Enter valid Data");
                    return
                }


                $http({
                    method: 'POST',
                    url: '/api/TimeSheetCalculate',
                    data: getSelectedItem()
                }).then(function successCallback(response) {
                    vm.timeSheetList = response.data;
                    console.log(response);

                },
                    function errorCallback(response) {
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                    });
            };

            vm.addRow = function (currentRow) {
                vm.isLoading = true;

                $http({
                    method: 'POST',
                    url: '/api/TimeSheetAddNew',
                    data: currentRow
                }).then(function successCallback(response) {
                    vm.isLoading = false;
                    vm.gettimeSheetList();

                },
                    function errorCallback(response) {
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                        vm.isLoading = false;
                    });

            };


            vm.checkAll = function () {

                angular.forEach(vm.timeSheetList, function (timeSheetList) {
                    timeSheetList.select = vm.selectAll;


                    //vm.totalNH = vm.totalNH + parseInt(timeSheetList.TotalNHmin)
                });

            };

            vm.UncheckAll = function () {

                angular.forEach(vm.timeSheetList, function (timeSheetList) {
                    timeSheetList.select = false;

                });

            };


            getSelectedItem = function () {
                var selecteItemList = [];
                angular.forEach(vm.timeSheetList, function (timeSheetList) {
                    if (timeSheetList.select == true) {
                        selecteItemList.push(timeSheetList);
                    }
                });

                return selecteItemList;
            }


            vm.process = function () {



                var _Option = {
                    headerText: 'Alert!',
                    bodyText: "Process Data Delete All Data if Exit",
                    closeButtonText: 'Cancel',
                    actionButtonText: 'Ok'
                };


                vm.showAlertMessages(_Option).then(
                    function () {
                        //console.log("ok")


                        vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                        var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                        var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;

                        if (!vm.selectedEmp) {

                            toaster.pop('error', "Error", "Please select Employee");
                        }

                        $http({
                            method: 'GET',
                            url: '/api/TimeSheetProcess',
                            params: { EmpCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                        }).then(function successCallback(response) {

                            vm.gettimeSheetList();

                        },
                            function errorCallback(response) {
                                console.log(response);
                                toaster.pop('error', "Error", response.data.Message);
                            });


                    },
                    function () {
                        console.log("error")
                        return;
                    });







            }

            vm.save = function () {

                if (getSelectedItem().length <= 0) {
                    toaster.pop('error', "Error", "Please Select Records");
                    return;
                }
                if (vm.timesheetform.$invalid) {
                    toaster.pop('error', "Error", " Please Enter valid Data");
                    return
                }
                $http({
                    method: 'POST',
                    url: '/api/TimeSheetUpdate',
                    data: getSelectedItem()
                }).then(function successCallback(response) {
                    toaster.pop('success', "Success", "Updated Successfuly");

                },
                    function errorCallback(response) {
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                    });


            }
            //getDateist
            vm.getdata = function () { 



                vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;


                $http({
                    method: 'GET',
                    url: '/api/GetDateList',
                    params: { fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {
                        console.log(response);
                        vm.endDateList = response.data;

                        vm.gettimeSheetList();
                    },
                    function errorCallback(response) {
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                    });





                // Get all options select
                //s = angular.element(e).find('option');
                //    console.log(s);

                //// Loop
                //angular.forEach(s, function (v, k) {
                //    console.log(v);
                //    // Is option selected..?
                //    if (angular.element(v).prop('selected')) {
                //        // Get Text option selected
                //        console.log('Text: ' + angular.element(v).text()); // Text
                //    }
                //});

            };
            //getproject
            vm.getSubproject = function () {

                $http({
                    method: 'GET',
                    url: '/api/SubProject',
                    params: { CompanyId: 4 }
                }).then(function successCallback(response) {
                    console.log(response);
                    vm.supProjectList = response.data;
                },
                    function errorCallback(response) {
                        //   console.log(response);
                        toaster.pop('error', "Error", response);
                    });

            }

            //getEmpList
            vm.getEmpList = function () {

                $http({
                    method: 'GET',
                    url: '/api/EmpList',
                    params: { CompanyId: 4 }
                }).then(function successCallback(response) {
                    // console.log(response);
                    vm.EmpList = response.data;
                },
                    function errorCallback(response) {
                        //   console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                    });
            };

            //get company value from session
            vm.companyValue = function (companyId, empCode) {

                vm.companyId = companyId;
                vm.loginUser = empCode;
            }

            vm.submit = function () {
                if (getSelectedItem().length <= 0) {
                    toaster.pop('error', "Error", "Please Select Records");
                    return;
                }

                if (vm.timesheetform.$invalid) {
                    toaster.pop('error', "Error", " Please Enter valid Data");
                    return
                }


                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;


                var selectList = [];
                angular.forEach(vm.timeSheetList, function (timeSheetList) {

                    if (timeSheetList.isValid == true && timeSheetList.IsOffDay == false && timeSheetList.select == true) {

                        selectList.push(timeSheetList);


                    }
                })




                $http({
                    method: 'POST',
                    url: '/api/TimesheetSubmit',
                    data: {
                        timeSheetlist: getSelectedItem(), empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate, companyId: vm.companyId
                    }

                }).then(function successCallback(response) {

                    if (vm.files) {
                        vm.uploadfilewhenSubmit(response.data.RefID);
                    }

                   
                    console.log(response);
                    toaster.pop('success', "Success", response.data.SucessMessage);
                    vm.gettimeSheetList();
                },
                    function errorCallback(response) {
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                    });
            }

            vm.unLock = function () {

                if (getSelectedItem().length <= 0) {
                    toaster.pop('error', "Error", "Please Select Records");
                    return;
                }

                $http({
                    method: 'POST',
                    url: '/api/TimesheetUnlock',
                    data: getSelectedItem()
                }).then(function successCallback(response) {

                    console.log(response);
                    vm.gettimeSheetList();
                },
                    function errorCallback(response) {
                        console.log(response);
                        toaster.pop('error', "Error", response.data.Message);
                    });
            }

            vm.delete = function () {



                if (getSelectedItem().length <= 0) {
                    toaster.pop('error', "Error", "Please Select Records");
                    return;
                }


                var _Option = {
                    headerText: 'Alert!',
                    bodyText: "Are You Sure Delete Data",
                    closeButtonText: 'Cancel',
                    actionButtonText: 'Ok'
                };


                vm.showAlertMessages(_Option).then(
                    function () {
                        console.log("ok")

                        $http({
                            method: 'POST',
                            url: '/api/TimesheetDelete',
                            data: getSelectedItem()
                        }).then(function successCallback(response) {
                            //vm.timeSheetList = response.data;
                            //  console.log(response);
                            vm.gettimeSheetList();
                        },
                            function errorCallback(response) {
                                console.log(response);
                                toaster.pop('error', "Error", response.data.Message);
                            });
                    }, function () {
                        return;

                    }
                );
            }

            //fill inout time for selected rows
            vm.keyinoutTime = function () {

                if (getSelectedItem().length <= 0) {
                    toaster.pop('error', "Error", "Please Select Records");
                    return;
                }

                //var inputdate = $document[0].getElementById('DeftxtInTime').value;
                //var outdate = $document[0].getElementById('DeftxtOutTime').value;
                var inputdate = vm.keyintime;
                var outdate = vm.keyouttime;
                console.log(inputdate);
                angular.forEach(getSelectedItem(), function (timeSheetList) {

                    timeSheetList.CheckInTime = inputdate;
                    timeSheetList.CheckOutTime = outdate;
                })
            };

            vm.GetIsTimeSheetSubmited = function () {

                vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;
                
                $http({
                    method: 'GET',
                    url: '/api/IsTimeSubmited',
                    params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {
                        console.log(response);
                        vm.IsSubmit = response.data;


                    },
                    function errorCallback(response) {
                        vm.IsSubmit = true;
                        //console.log(response);
                        //toaster.pop('error', "Error", response.data);
                    });




            }




            vm.GetIsTimeSheetApproved = function () {

                vm.IsApproved = false;
                if (vm.timeSheetList[0] == null)
                    return;

                        var _refId = vm.timeSheetList[0].Refid;


                        if (_refId != null) {



                            $http({
                                method: 'GET',
                                url: '/api/IsTimeApproved',
                                params: { refid: _refId }
                            }).then(
                                function successCallback(response) {
                                    console.log(response.data);
                                    vm.IsApproved = response.data;


                                },
                                function errorCallback(response) {
                                    vm.IsApproved = false;
                                    //console.log(response);
                                    //toaster.pop('error', "Error", response.data);
                                });
                        }
                              

            }

            calculateTotal = function () {


                vm.totalNH = 0;
                vm.totalOT1 = 0;
                vm.totalOT2 = 0;
                vm.totalLateNess = 0;

                angular.forEach(vm.timeSheetList, function (timeSheetList) {

                    if (timeSheetList.isValid == true && timeSheetList.IsOffDay == false) {
                        vm.totalNH = vm.totalNH + parseInt(timeSheetList.TotalNHmin);
                        vm.totalOT1 = vm.totalOT1 + parseInt(timeSheetList.TotalOt1Min)
                        vm.totalOT2 = vm.totalOT2 + parseInt(timeSheetList.TotalOt2Min)
                        vm.totalLateNess = vm.totalLateNess + parseInt(timeSheetList.LateMin)

                    }
                })


            }

            vm.formatekeyIntime = function (data) {

                //var value=   parseInt(data)
                //   console.log(value)

                if (!isNaN(data)) {

                    vm.keyintime = hours_am_pm(data);

                }

            }
            vm.formatekeyOuttime = function (data) {

                //var value=   parseInt(data)
                //   console.log(value)

                if (!isNaN(data)) {

                    vm.keyouttime = hours_am_pm(data);

                }

            }

            vm.formateIntime = function (data, index, control) {

                //var value=   parseInt(data)
                //   console.log(value)



                if (!isNaN(data)) {

                    vm.timeSheetList[index].CheckInTime = hours_am_pm(data);
                    vm.validateTimeSheet();



                    console.log();
                    //  calculateTimeSheet(vm.timeSheetList[index])
                }





            }
            vm.formateOuttime = function (data, index, control) {

                if (!isNaN(data)) {

                    vm.timeSheetList[index].CheckOutTime = hours_am_pm(data);
                    vm.validateTimeSheet();

                    var elemet = '#5' + vm.timeSheetList[index + 1].Pk;
                    console.log(elemet)
                    var nextelement = angular.element.find(elemet);
                    console.log(nextelement)
                    nextelement[0].focus();
                    nextelement[0].select();

                    //calculateTimeSheet(vm.timeSheetList[index])
                }


            }

            function hours_am_pm(data) {

                if (data) {



                    var length = data.length;

                    var n1 = data.split('_');

                    var time = n1[0] + n1[1];


                    var hours = time[0] + time[1];
                    var min = time[2] + time[3];

                    console.log("TIME[0]" + time[0])
                    console.log("TIME[1]" + time[1])
                    console.log("time[2]" + time[2])
                    console.log("time[3]" + time[3])


                    if (length == 3) {
                        console.log(length)
                        hours = time[0];
                        min = time[1] + time[2];
                    }





                    if (length == 1) {
                        console.log(length)
                        hours = time[0];
                        min = "00";
                    }

                    if (length == 2) {
                        console.log(length)
                        hours = time[0] + time[1];
                        min = "00";
                    }



                    if (hours < 10 && hours.length < 2) {

                        hours = '0' + hours;
                    }




                    else if (hours > 23) {
                        hours = '00';
                    }

                    if (min < 10 && min.length < 2) {
                        min = '0' + min;
                    }
                    else if (min > 59) {
                        min = "00";
                    }

                    //if (hours < 12) {
                    //   // return hours + ':' + min + ' AM';
                    //    console.log("lessthan 12")
                    //    return hours + ':' + min;
                    //} else {
                    //    //hours = hours - 12;

                    //    console.log(">12")
                    //    hours = (hours.length < 2) ? '0' + hours : hours;
                    //    //return hours + ':' + min + ' PM';
                    //    return hours + ':' + min;
                    //}

                    return hours + ':' + min;
                }
            }



            addWatch = function () {


                for (var ii = 0; ii < vm.timeSheetList.length; ii++) {



                    //$scope.$watchGroup(['vm.timeSheetList[' + ii + '].CheckInDate',
                    //    'vm.timeSheetList[' + ii + '].CheckOutDate',
                    //    'vm.timeSheetList[' + ii + '].CheckInTime',
                    //    'vm.timeSheetList[' + ii + '].CheckOutTime'], function (newValues, oldValues, scope) {

                    $scope.$watch('vm.timeSheetList[' + ii + ']', function (newValues, oldValues, scope) {


                        if (oldValues.select != newValues.select) {

                            return;
                        }

                        vm.validateTimeSheet();

                        if (newValues.isValid == true) {

                            vm.calculateTimeSheet(newValues)
                        }

                        calculateTotal();


                        //var inputTimeElement = vm.timesheetform['5' + model.Pk]

                        //var outTimeElement = vm.timesheetform['6' + model.Pk]


                        //var CheckInTimeDate = '2017-06-01T' + model.CheckInTime;
                        //var CheckInTime = new Date(CheckInTimeDate);

                        //var CheckOutTimeDate = '2017-06-01T' + model.CheckOutTime;
                        //var CheckOutTime = new Date(CheckOutTimeDate);

                        //console.log(CheckInTimeDate)
                        //if (angular.isDate(CheckInTimeDate) && angular.isDate(CheckOutTimeDate)) {

                        //   if (validateHhMm(model.CheckInTime) && validateHhMm(model.CheckOutTime)) {


                        //validateTimeSheet(vm.timeSheetList)
                        //    .then(function successCallback(response) { },
                        //    function errorCallback(response) {

                        //    });




                        //  var id = vm.timeSheetList[0];
                        //  console.log(id);

                        //  var index = vm.timeSheetList.indexOf(model)
                        //var  _previous = vm.timeSheetList[index - 1];

                        //  var list = {
                        //      TimeSheet: model,
                        //      Previous: _previous,
                        //      Next: null
                        //  }


                        //$timeout(function () {


                        //if (model.isValid == true) {

                        //claculateSingle(model).then(function successCallback(response) {

                        //    //vm.timeSheetList[index].TotalNHmin = response.data[0].TotalNHmin;
                        //    //vm.timeSheetList[index].TotalOt1Min
                        //    //vm.timeSheetList[index].TotalOt2Min
                        //    //vm.timeSheetList[index].isValid



                        //    model.TotalNHmin = response.data[0].TotalNHmin;
                        //    model.TotalOt1Min = response.data[0].TotalOt1Min;
                        //    model.TotalOt2Min = response.data[0].TotalOt2Min;


                        //    //_previous.isValid = true;
                        //    model.isValid = true;


                        //    //console.log(response);
                        //    //inputTimeElement.$setValidity('range', true)
                        //},
                        //    function errorCallback(response) {
                        //        console.log(response.data.Message);
                        //        //toaster.pop('error', "Error", response);
                        //        model.isValid = false;
                        //        _previous.isValid = false;
                        //        model.ErroMsg = response.data.Message;
                        //    });
                        //  },200)






                        //}

                        //}
                        //else {
                        //    model.TotalNHmin = 0;
                        //    model.TotalOt1Min = 0;
                        //    model.TotalOt2Min = 0;
                        //    model.isValid = false;
                        //}





                    }, true)



                }
            }

            claculateSingle = function (timeSheet) {


                return $http({
                    method: 'POST',
                    url: '/api/CalculateTimesheet',
                    data: timeSheet
                })
            };


            vm.calculateTimeSheet = function (model) {

                if (validateHhMm(model.CheckInTime) && validateHhMm(model.CheckOutTime)) {


                    //console.log("http")

                    //$timeout(function () {


                    if (model.isValid == true) {

                        claculateSingle(model).then(function successCallback(response) {

                           
                         //   model = response.data[0];
                          //  console.log(model);
                            model.TotalNHmin = response.data[0].TotalNHmin;
                            model.TotalOt1Min = response.data[0].TotalOt1Min;
                            model.TotalOt2Min = response.data[0].TotalOt2Min;
                            model.LateMin = response.data[0].LateMin;
                            model.RIntime = response.data[0].RIntime;
                            model.ROuttime = response.data[0].ROuttime;
                            model.REarlyIn = response.data[0].REarlyIn;
                            model.REarlyOut = response.data[0].REarlyOut;
                            model.RosterType = response.data[0].RosterType;
                            model.RLate = response.data[0].RLateIn;
                            //model.
                            //      model.isValid = true;


                            //console.log(response);
                            //inputTimeElement.$setValidity('range', true)
                        },
                            function errorCallback(response) {
                                console.log(response.data.Message);
                                //toaster.pop('error', "Error", response);
                                //   model.isValid = false;

                                model.ErroMsg = response.data.Message;
                            });
                        //  },200)






                    }

                }
                else {
                    model.TotalNHmin = 0;
                    model.TotalOt1Min = 0;
                    model.TotalOt2Min = 0;
                    model.isValid = false;
                }


            }



            vm.validateTimeSheet = function () {


                var periveyas;
                angular.forEach(vm.timeSheetList, function (ti) {

                    ti.isValid = true;
                    ti.isNightShift = true;

                    if (!ti.CheckInTime) {
                        ti.ErroMsg = "Intime Empty";
                        ti.isValid = false;
                        return;
                    }

                    if (!ti.CheckOutTime) {
                        ti.ErroMsg = "Intime Empty";
                        ti.isValid = false;
                        return;
                    }




                    var intime = new Date((new Date(ti.CheckInDate).toLocaleDateString()) + ' ' + ti.CheckInTime);

                    var outtime = new Date((new Date(ti.CheckOutDate).toLocaleDateString()) + ' ' + ti.CheckOutTime)

                    


                    if (intime > outtime) {
                        ti.isValid = false;
                        ti.ErroMsg = "Intime > Outtime";

                    }




                    if (periveyas > intime) {
                        ti.isValid = false;
                        ti.ErroMsg = "Time OverLap";

                    }
                    //if (ti.IsOffDay == true) {
                    //    ti.isValid = false;
                    //    ti.ErroMsg = "Off/Leave";
                    //}

                   
                    var diffhour = Math.abs(intime - outtime) / 36e5;

                    if (diffhour > 24) {
                        ti.isValid = false;
                        ti.ErroMsg = "Exceed 24 Hours";
                    }


                    if (ti.CheckInDate === ti.CheckOutDate) {
                        ti.isNightShift = false;
                    }


                    periveyas = outtime;
                });

                //var timeSheetList = vm.timeSheetList;
                //return $http({
                //    method: 'POST',
                //    url: '/api/ValidateTimesheet',
                //    data: timeSheetList
                //})


                //console.log(vm.timeSheetList);



            };


            




            vm.changeProject = function (index) {

                var selectedvalue = vm.timeSheetList[index].SubProjectId;

                angular.forEach(vm.timeSheetList, function (timeSheetList, $index) {
                    if (vm.projectChangeAll == true) {
                        if ($index > index) {

                            timeSheetList.SubProjectId = selectedvalue;
                        }
                    }


                });


            }





            function validateHhMm(inputField) {
                var isValid = /^([0-1]?[0-9]|[2][0-3]):([0-5][0-9])(:[0-5][0-9])?$/.test(inputField);



                return isValid;
            }




            vm.changeNightShift = function () {

                if (vm.nightShiftChange === true) {



                    $http({
                        method: 'POST',
                        url: '/api/ChangeNightShift',
                        data: vm.timeSheetList
                    }).then(function successCallback(response) {

                        vm.timeSheetList = response.data;

                    },
                        function errorCallback(response) {
                            console.log(response);
                            toaster.pop('error', "Error", response.data.Message);
                        });

                }
                else {

                    angular.forEach(vm.timeSheetList, function (list) {

                        list.CheckOutDate = list.CheckInDate;
                    })
                }
            }

            vm.UploadFileInfo = function () {
                vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;

                $http({
                    method: 'GET',
                    url: '/api/GetRefID',
                    params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {

                        console.log(response.data);
                        vm.serverFiles = response.data.FileName;
                        vm.refId = response.data.RefId;
                    }
                    , function (err) {

                       // toaster.pop('error', "Error", err.data.Message);
                    });
            }

            vm.uploadfile = function ()
            {

                vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;
                
                $http({
                    method: 'GET',
                    url: '/api/GetRefID',
                    params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {

                        console.log(response.data);

                        var refId = response.data.RefId;


                        var _empId = vm.selectedEmp.value;

                  Upload.upload({
                    url: "/api/TimeSheetUploadFile",
                    data: { file: vm.files, empId: _empId, companyId: vm.companyId ,refId:refId}
                    })
                    .then(function (response) {
                        console.log(response);
                        vm.serverFiles = response.data.fileName;
                        toaster.pop('success', "Success", " uploded Successfuly");

                    }, function (err) {
                      
                        toaster.pop('error', "Error", "Please Submit First");
                    });



                    },
                    function errorCallback(response) {
                        
                        toaster.pop('error', "Error", "Please Submit First");
                    });

            }
                 
            vm.uploadfilewhenSubmit = function (_refId) {

                console.log(_refId);

                        var refId = _refId


                        var _empId = vm.selectedEmp.value;

                        Upload.upload({
                            url: "/api/TimeSheetUploadFile",
                            data: { file: vm.files, empId: _empId, companyId: vm.companyId, refId: refId }
                        })
                            .then(function (response) {
                                console.log(response);
                                vm.serverFiles = response.data.fileName;
                                toaster.pop('success', "Success", " uploded Successfuly");

                            }, function (err) {

                                toaster.pop('error', "Error", "Please Submit First");
                            });

                
                   

            }

            vm.removefile = function () {


                vm.files = null;

                vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;

                $http({
                    method: 'Get',
                    url: '/api/GetRefID',
                    params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {

                  
                        var refId = response.data.RefId;
                        var _empId = vm.selectedEmp.value;

                       
                        $http({
                            method: 'GET',
                            url: '/api/TimeSheetDeleteFile',

                            params: { refId: refId, empcode: _empId, companyId: vm.companyId }
                        }).success(function (data, status, headers, cfg) {

                            vm.serverFiles = null;
                        }


                        ).error(function (err, status) {


                            alert("No File Deleted");

                        });


                    },
                    function errorCallback(response) {

                        console.log(response);
                    });




               

            }

            ////neverUse
            //vm.GetRefID = function () {

            //    vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

            //    var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
            //    var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;

            //    $http({
            //        method: 'GET',
            //        url: '/api/GetRefID',
            //        params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
            //    }).then(
            //        function successCallback(response) {

            //            console.log(response.data);

            //            vm.refId = response.data.RefId;






            //        },
            //        function errorCallback(response) {

            //            toaster.pop('error', "Error", "Please Submit First");
            //        });

            //}




            //never use
            vm.getOffDays = function () {

                vm.selectedEmp = JSON.parse($document[0].getElementById('RadComboBoxEmpPrj_ClientState').value);

                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;






                $http({
                    method: 'GET',
                    url: '/api/GetOffDays',
                    params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {

                        var offday = response.data;

                        angular.forEach(offday, function (off) {


                            console.log(off)
                            angular.forEach(vm.timeSheetList, function (ti) {



                                var offday = new Date(off.StartTime.getFullYear(), off.StartTime.getMonth(), off.StartTime.getDate());

                                var tiDay = new Date(ti.StartTime.getFullYear(), ti.StartTime.getMonth(), ti.StartTime.getDate());



                                if (offday === tiDay)
                                    ti.isOffDay = true;


                            });
                        });


                    },
                    function errorCallback(response) {
                        vm.IsSubmit = true;
                        //console.log(response);
                        //toaster.pop('error', "Error", response.data);
                    });
            };

            //never use
            vm.PuplicHolyDays = function () {



                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;






                $http({
                    method: 'GET',
                    url: '/api/GetPuplicHolyDays',
                    params: { companyId: vm.companyId, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {

                        var phday = response.data;


                        angular.forEach(phday, function (ph) {



                            angular.forEach(vm.timeSheetList, function (ti) {



                                var pday = new Date(ph.HolidayDate);

                                var tiDay = new Date(ti.CheckInDate);

                                console.log(pday)
                                console.log(tiDay)

                                if (pday.getTime() === tiDay.getTime()) {
                                    ti.isPhDay = true;

                                }

                            });
                        });


                    },
                    function errorCallback(response) {
                        vm.IsSubmit = true;
                        //console.log(response);
                        //toaster.pop('error', "Error", response.data);
                    });
            };


            //never use
            vm.leaveDays = function () {



                var inputdate = $document[0].getElementById('rdEmpPrjStart_dateInput_text').value;
                var outdate = $document[0].getElementById('rdEmpPrjEnd_dateInput_text').value;






                $http({
                    method: 'GET',
                    url: '/api/GetleaveDays',
                    params: { empCode: vm.selectedEmp.value, fromDate: inputdate, endDate: outdate }
                }).then(
                    function successCallback(response) {

                        var leaveday = response.data;


                        angular.forEach(leaveday, function (ph) {



                            angular.forEach(vm.timeSheetList, function (ti) {



                                var lday = new Date(leaveday);

                                var tiDay = new Date(ti.CheckInDate);

                                //console.log(pday)
                                //console.log(tiDay)

                                if (lday.getTime() === lday.getTime()) {
                                    ti.IsLeaveDay = true;

                                }

                            });
                        });


                    },
                    function errorCallback(response) {
                        vm.IsSubmit = true;
                        //console.log(response);
                        //toaster.pop('error', "Error", response.data);
                    });
            };









        }]);


