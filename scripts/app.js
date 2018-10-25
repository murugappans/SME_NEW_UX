


if (!Date.prototype.toISOStrin) {
    (function () {

        function pad(number) {
            if (number < 10) {
                return '0' + number;
            }
            return number;
        }

        Date.prototype.toISOStrin = function () {
            return this.getUTCFullYear() +
                '-' + pad(this.getUTCMonth() + 1) +
                '-' + pad(this.getUTCDate()) +
                'T00:00:00';

        };

    }());
}


angular.module('cliamApp', ['toaster', 'ngFileUpload', 'ui.bootstrap', '720kb.datepicker'])



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
    //.directive('myCustomUrl', function () {
    //    return {
    //        templateUrl: '/Default/index'
    //    };
    //})
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

    .directive('formattedDate', function (dateFilter) {
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
    })
    .controller('mainController', ['$http', 'toaster', '$log', '$window', 'Upload', '$modal', function ($http, toaster, $log, $window, Upload, $modal) {



        var vm = this;

        vm.empCode = '';
        vm.companyId = 0;
        vm.multiCurrency = false;
        vm.GstCode = "GST";
        vm.GstRate = 0;


        //Enable
        vm.projectShow = false;
        vm.TaxShow = false;
        vm.ReciptShow = false;
        vm.DisciptionShow = false;
        vm.AttacementShow = false;
        vm.RemarkShow = false;
        vm.IncurredDateShow = false;
        vm.claimCappingShow = false;

        //Required
        vm.projectRequired = false;
        vm.TaxRequired = false;
        vm.ReciptRequired = true;
        vm.DisciptionRequired = true;
        vm.AttacementRequired = true;
        vm.RemarkRequired = true;
        vm.IncurredDateRequired = true;






        vm.companyValue = function (companyId, empCode, multiCurrency) {

            vm.companyId = companyId;
            vm.empCode = empCode;
            vm.multiCurrency = multiCurrency;
            vm.TaxPersentage = 0.0;

            $http.get('/api/CliamEmpList', {
                params: { companyId: vm.companyId, loginId: vm.empCode }
                // headers: { 'Authorization': 'Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==' }
            }
            )
                .then(function successCallback(response) {
                    console.log(response);
                    vm.EmpList = response.data;
                },
                    function errorCallback(response) {
                        console.log(response);
                    });



            //get Tax 

            $http.get('/api/GetTax', {
                params: { companyId: vm.companyId }
                // headers: { 'Authorization': 'Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==' }
            }
            )
                .then(function successCallback(response) {

                    vm.GstCode = response.data.TaxCode;
                    vm.GstRate = (response.data.TaxPersendage / 100);

                    console.log(vm.GstRate);
                },
                    function errorCallback(response) {
                        console.log(response);
                    });



            //getSubProject


            $http({
                method: 'GET',
                url: '/api/SubProject?companyId=' + vm.companyId
            }).then(function successCallback(response) {
                console.log(response);
                vm.subProject = response.data;
            },
                function errorCallback(response) {
                    console.log(response);
                    toaster.pop('error', "Error", response);
                });





            //getDateList

            $http({
                method: 'GET',
                url: '/api/DateList',
                params: { month: vm.month, year: vm.year }
            }).then(function successCallback(response) {
                console.log(response);
                vm.dateList = response.data;
            },
                function errorCallback(response) {
                    console.log(response);
                    //toaster.pop('error', "Error", response);
                });


            $http({
                method: 'GET',
                url: '/api/GetClaimSettings',
                params: { companyId: vm.companyId }
            }).then(function successCallback(response) {

                console.log(response.data);


                response.data.forEach(myFunction);

                function myFunction(item, index) {

                    if (item.FieldName === 'Project') {

                        vm.projectShow = item.Enable;
                        vm.projectRequired = item.Required;
                        console.log(vm.projectRequired);
                    }
                    else if (item.FieldName === 'ReceiptNo') {
                        vm.ReciptRequired = item.Required;
                        vm.ReciptShow = item.Enable;

                        console.log(item.Enable);
                    } else

                        if (item.FieldName === 'ClaimDiscription') {

                            vm.DisciptionShow = item.Enable;
                            vm.DisciptionRequired = item.Required;
                            console.log(item.Enable);
                        } else
                            if (item.FieldName === 'AdditionalRemark') {

                                vm.RemarkShow = item.Enable;
                                vm.RemarkRequired = item.Required;
                                console.log(item.Enable);
                            } else
                                if (item.FieldName === 'Attachment') {

                                    vm.AttacementShow = item.Enable;
                                    vm.AttacementRequired = item.Required;
                                    console.log(item.Enable);
                                } else
                                    if (item.FieldName === 'IncurredDate') {

                                        vm.IncurredDateShow = item.Enable;
                                        vm.IncurredDateRequired = item.Required;
                                        console.log(item.Enable);
                                    } else
                                        if (item.FieldName === 'Tax') {

                                            vm.TaxShow = item.Enable;
                                            vm.TaxRequired = item.Required;
                                            console.log(item.Enable);
                                        };

                }
            },
                function errorCallback(response) {
                    console.log(response);
                    //toaster.pop('error', "Error", response);
                });





        }


        vm.month = '0' + (new Date().getMonth() + 1);
        vm.year = new Date().getFullYear();

        vm.EmpList = [];

        vm.CliamTypes = [];

        vm.subProject = [];

        vm.ClaimTypeCapAmount = [{}];

        vm.dateList = [];

        vm.ClaimList = [];




        vm.loadClaimType = function () {

            if (!vm.selectedEmp) {
                toaster.pop('error', "Error", "please  select Employee");

                return;
            }
            //getCliamType
            $http({
                method: 'GET',
                url: '/api/CliamTypeListByempcode?companyId=' + vm.companyId + '&empcode=' + vm.selectedEmp.EmpCode
            }).then(function successCallback(response) {
                console.log(response);
                vm.CliamTypes = response.data;
            },
                function errorCallback(response) {
                    console.log(response);
                    toaster.pop('error', "Error", response);
                });

            //getCliamType
            $http({
                method: 'GET',
                url: '/api/IsCliamCappingForEmploye?EmpId=' + vm.selectedEmp.EmpCode
            }).then(function successCallback(response) {
                console.log(response);
                vm.claimCappingShow = response.data;
            },
                function errorCallback(response) {
                    console.log(response);
                    toaster.pop('error', "Error", response);
                });


            vm.ClaimList = [];

        }

        //fileUpload

        vm.uploadFile = function uploadFiles(item) {

            var files = item.files;



            Upload.upload({
                url: "/api/UploadFile",
                data: { file: files, empId: vm.selectedEmp.EmpCode, companyId: vm.companyId }
            })
                .then(function (response) {
                    console.log(response);
                    item.Recpath = response.data.fileName;
                    item.Recpyn = 1;
                    vm.apply(item);
                    toaster.pop('success', "Success", "Receipt uploded Successfuly");

                }, function (err) {
                    console.log(err);
                    toaster.pop('error', "Error", err.data.Message);
                });


        }


        //getApplied Cliams

        vm.getCliams = function () {

            return;//lbd don't want show applied cliams


            if (!vm.selectedEmp) {
                toaster.pop('error', "Error", "please  select Employee");

                return;
            }

            if (!vm.month) {
                toaster.pop('error', "Error", "please  select month");

                return;
            }

            if (!vm.year) {
                toaster.pop('error', "Error", "please  select year");

                return;
            }


            vm.getDateList();

            $http({
                method: 'GET',
                url: '/api/GetAppliedClaims',
                params: { companyId: vm.companyId, empId: vm.selectedEmp.EmpCode, month: vm.month, year: vm.year }
            }).then(function successCallback(response) {
                console.log(response);
                vm.ClaimList = response.data;
            },
                function errorCallback(response) {
                    console.log(response);
                    toaster.pop('error', "Error", response);
                });


        }


        // Add New Claim
        vm.addNew = function () {

            //  vm.selectAll = true;
            console.log(new Date().toISOStrin());

            if (!vm.selectedEmp) {
                //  toaster.pop('error', "Error","please  select Employee");


                alert("please  select Employee");
                return;
            }

            if (!vm.month) {
                //toaster.pop('error', "Error","please  select month");

                alert("please  select month");
                return;
            }

            if (!vm.year) {
                //  toaster.pop('error', "Error","please  select year");
                alert("please  select year");
                return;
            }


            if (vm.ClaimList != null && vm.ClaimList.length == 1 && vm.claimCappingShow === 'true') {
                alert("You can not add more than cliam ");
                return;
            }


            vm.getDateList();

            //New Cliam

            isPayrollProcced = {

                companyId: vm.companyId, empid: vm.selectedEmp.EmpCode, date: '1/' + vm.month + '/' + vm.year
            }


            $http({
                method: 'post',
                url: '/api/IsPayrollProcessedMonth',
                // params: { companyId: vm.companyId, empId: vm.selectedEmp.EmpCode, date: '1/' + vm.month + '/' + vm.year }
                data: isPayrollProcced

            }).success(function (data, status, headers, cfg) {
                console.log(vm.empCode);

                vm.newClaim = {
                    Bid: 1,
                    Claimstatus: null,
                    CompanyId: 4,
                    ConversionOpt: 1,
                    CreatedBy: vm.empCode,
                    CreatedOn: null,
                    CurrencyId: 1,
                    CurrencyIdComp: null,
                    Desc: null,
                    EmpCode: vm.selectedEmp.EmpCode,
                    ExRate: 1,
                    GlAcc: null,
                    GstAmnt: 0.0,
                    GstRate: 1,
                    GstCode: "GST",
                    GstFlag: "0",
                    Id: 0,
                    ModifiedBy: null,
                    ModifiedOn: null,
                    PayAmount: 0,
                    Recpath: null,
                    Recpyn: 0,
                    Remarks: null,
                    SrNo: 1,
                    ToatlBefGst: 0.00,
                    ToatlWithGst: 0.0,
                    TransId: 0,
                    ProjectId: -1,
                    TrxPeriod: new Date().toISOStrin(),
                    TrxType: null,
                    TrxCapAmount: -1,
                    MonyhlyCapping: -1,
                    YearlyCapping: -1,
                    IncurredDate: '',
                    Description: '',
                    ReceiptNo: ''




                }


                if (vm.ClaimList == null) {
                    vm.ClaimList = [vm.newClaim];
                } else {
                vm.ClaimList.push(vm.newClaim);

                    // vm.checkAll();
                }


            }).error(function (err, status) {

                //  toaster.pop('error', "Error", err);


                alert("Payroll has been processed for the month.");


            });








        }


        vm.checkAll = function () {

            if (vm.selectAll) {
                vm.selectAll = false;
            } else {
                vm.selectAll = true;
            }


            angular.forEach(vm.ClaimList, function (item) {

                //console.log(vm.selectAll);
                //if (item.select) {
                //    console.log(item.select);
                //    item.select = true;
                //}
                //else {
                item.select = vm.selectAll;
                //  }



                //vm.totalNH = vm.totalNH + parseInt(timeSheetList.TotalNHmin)
            });

        };





        vm.IsPayrollProcced = function () {

            if (!vm.selectedEmp) {
                toaster.pop('error', "Error", "please  select Employee");

                return;
            }

            if (!vm.month) {
                toaster.pop('error', "Error", "please  select month");

                return;
            }

            if (!vm.year) {
                toaster.pop('error', "Error", "please  select year");

                return;
            }


            var date = new Date();

            $http({
                method: 'GET',
                url: '/api/IsPayrollProcessed',
                params: { companyId: vm.companyId, empId: vm.selectedEmp.EmpCode, trxDate: '1/' + vm.month + '/' + vm.year }

            }).then(function successCallback(response) {
                console.log(response);


            },
                function errorCallback(response) {
                    console.log(response);
                    toaster.pop('error', "Error", "PayRoll Already processed");
                });

        }


        vm.getDateList = function () {

            $http({
                method: 'GET',
                url: '/api/DateList',
                params: { month: vm.month, year: vm.year }
            }).then(function successCallback(response) {
                console.log(response);
                vm.dateList = response.data;
            },
                function errorCallback(response) {
                    console.log(response);
                    toaster.pop('error', "Error", response);
                });
        }



        vm.GetCliamTypeAmount = function (cliamTypeId, empId, item) {


            $http.get('/api/GetCliamCappingAmount', {
                params: {
                    companyId: vm.companyId,
                    empId: empId,
                    cliamTypeid: cliamTypeId,
                    date: item.TrxPeriod

                }
                // headers: { 'Authorization': 'Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==' }
            }
            )
                .then(function successCallback(response) {
                    console.log(response);

                    if (response.data.transaction != -1) {
                        item.TrxCapAmount = response.data.Transection;
                        item.MonthlyCapAmount = response.data.Monthly;
                        item.YearlyCapAmount = response.data.Yearly;
                    }

                },
                    function errorCallback(response) {
                        item.TrxCapAmount = 0;
                        item.MonthlyCapAmount = 0;
                        item.YearlyCapAmount = 0;
                        console.log(response);
                    });

        }


        //submit Cliam

        vm.submitClaim = function (claimList) {

            $http({
                method: 'POST',
                url: vm.baseUrl + '/api/submitCliam',

                data: claimList
            }).success(function (data, status, headers, cfg) {

                console.log(data);
                toaster.pop('success', "Success", "Data Saved Successfuly");
            }


            ).error(function (err, status) {

                toaster.pop('error', "Error", err);

            });


        }
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

            modalInstance.result.then(function () {
                return;
            }, function () {
                $log.info(' Error Modal dismissed at: ' + new Date());
            });
        };


        vm.applywithConfirm = function (formvalid) {

            if (formvalid) {
                var _Option = {
                    headerText: 'Alert!',
                    bodyText: "Please fill up all required fields.",
                    closeButtonText: 'Cancel',
                    actionButtonText: 'Ok'
                };


                vm.showAlertMessages(_Option);
                return;
            };

            var noOfselectedRow = 0;

            angular.forEach(vm.ClaimList, function (ClaimList) {
                if (ClaimList.select == true) {
                    noOfselectedRow = 1;
                }
            });



            if (noOfselectedRow == 0) {
                var _Option = {
                    headerText: 'Alert!',
                    bodyText: "Please select atleast one transection",
                    closeButtonText: 'Cancel',
                    actionButtonText: 'Ok'
                };


                vm.showAlertMessages(_Option);
                return;
            }

          



            if (vm.claimCappingShow === "true") {
                console.log(vm.claimCappingShow)
                var message = vm.getCapAmountMessage(vm.ClaimList[0]);

                if (message !== 'OK') {
                    var _Option = {
                        headerText: 'Proceed Claim Submission?',
                        bodyText: message,
                        closeButtonText: 'Cancel',
                        actionButtonText: 'Ok'
                    };
                    vm.showAlertMessages(_Option);
                    return;
                }
              
              
            };

            vm.Option = {
                headerText: 'Proceed Claim Submission?',
                bodyText: 'Apply Cliam?',
                closeButtonText: 'Cancel',
                actionButtonText: 'Ok'
            };

            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                controllerAs: '$ctrl',
                size: 'sm',
                resolve: {
                    item: function () {
                        return vm.Option;
                    }
                }
            });

            modalInstance.result.then(

                function () {
                    //   vm.apply(itemAdd);


                    angular.forEach(vm.ClaimList, function (ClaimList) {
                        if (ClaimList.select == true) {
                            if (ClaimList.files != null) {
                                vm.uploadFile(ClaimList);
                            } else {
                                vm.apply(ClaimList);
                            }
                        }
                    });



                },


                function () {
                    $log.info(' Error Modal dismissed at: ' + new Date());
                });



            //if ($window.confirm(message)) {

            //    if (item.files != null) {
            //        vm.uploadFile(item);
            //    } else {
            //        vm.apply(item);
            //    }



            //} else {
            //    console.log("no");
            //}

        };

        vm.removewithConfirm = function (itemdelete) {


            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                controllerAs: '$ctrl',
                size: 'sm',
                resolve: {
                    item: function () {
                        return vm.modelOption;
                    }
                }
            });

            modalInstance.result.then(function () {
                vm.remove(itemdelete);
            }, function () {
                $log.info(' Error Modal dismissed at: ' + new Date());
            });


            //var message = 'Are You Sure to Cancel ?';


            //if ($window.confirm(message)) {

            //    vm.remove(item);

            //} else {
            //    console.log("no");
            //}

        };


        vm.apply = function (claim) {

            console.log(claim);



            $http({
                method: 'POST',
                url: '/api/ApplyClaim',

                data: claim
            }).success(function (data, status, headers, cfg) {

                vm.ClaimList = null;

                //  vm.getCliams();

                // toaster.pop('success', "Success", "Data Saved Successfuly");
                alert(data);
            }


            ).error(function (err, status) {
                vm.getCliams();
                //  toaster.pop('error', "Error", err);
                console.log(err);
                alert(err.data.Message)
            });


        }



        vm.remove = function (item) {

            $http({
                method: 'POST',
                url: '/api/DeleteClaim',

                data: item
            }).success(function (data, status, headers, cfg) {


                vm.getCliams();
                //console.log(data);
                //toaster.pop('success', "Success", "Data Saved Successfuly");
            }


            ).error(function (err, status) {

                //console.log(err);

                //toaster.pop('error', "Error", err.Message);
                alert("Claim has been approved and cancellation is not allowed.");

            });

        }


        //calculate GST

        vm.Calculate = function (item) {



            if (item == null)
                return;

            item.PayAmount = item.ToatlWithGst;



            if (item.GstFlag != 1) {

                item.ToatlBefGst = 0;
                item.GstAmnt = 0;
                return;
            }


            if (item.ToatlWithGst > 0.0) {


                var k = 1 + vm.GstRate;

                console.log(k);

                item.ToatlBefGst = (item.ToatlWithGst / k).toFixed(2);
                item.GstAmnt = (item.ToatlWithGst - item.ToatlBefGst).toFixed(2);
            } else {

                item.ToatlBefGst = 0;
                item.GstAmnt = 0;
            }

        }


        vm.getCapAmountMessage = function (cliam) {

            console.log(cliam)

            var message='OK';


            if (cliam.TrxCapAmount != -1 || cliam.TrxCapAmount != 0) {

                if (cliam.ToatlWithGst > cliam.TrxCapAmount) {

                    return message = ' Applied amount exceeds the transaction limit.';

                }




                if (cliam.ToatlWithGst > cliam.MonthlyCapAmount) {
                    return message = ' Applied amount exceeds the monthly limit.';
                }


                if (cliam.ToatlWithGst > cliam.YearlyCapAmount) {


                    return message = ' Applied amount exceeds the yearly limit.';
                }



            }
            return message;

        }


        vm.canAddMultiClaim = function () {
            var result = true;
            if (vm.claimCappingShow) {
                result = false;
            }
            return result;
        }

    }]);