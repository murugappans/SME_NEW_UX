angular.module("timemodule")
    .directive('timex', function () {
        return {
            restrict: 'E',
            replace: true,
            template: '<Input type="text" ng-model="model" />',
            scope: {
                model: '=',
                timeMode: '='
            },
            link: timepickerLinkFn
        };


        function timepickerLinkFn (scope, element, attrs) {



            element.on('blur', function (data) {
                console.log(data);

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

            function formatter(value) {

                var formattedValueLength = timeFormat.length;
                var unformattedValueLength = timeFormat.replace(':', '').length;
                var timeMask = new String(timeFormat);

                if (ngModel.$isEmpty(value)) {
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
                    var timeRegex = /^(0?[0-9]|1[0-2]):[0-5][0-9] ?[a|p]m$/i;
                    if (!scope.showMeridian) {
                        timeRegex = /^([01]?[0-9]|2[0-3]):[0-5][0-9]$/;
                    }
                    if (!timeRegex.test(value)) {
                        ngModel.$setValidity('time', false);
                        return undefined;
                    }
                    else {
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
                timeFormat = (!scope.showMeridian) ? "HH:mm" : "hh:mm a";
                var date = formatter(value);
                return date;
            });

            ngModel.$parsers.push(function (value) {
                var date = showTime(value);
                return date;
            });


        } //link function
    };
})