var TableDatatablesEditable = function () {

    var handleTable = function (target) {

        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            for (var i = 0, iLen = jqTds.length; i < iLen; i++) {
                oTable.fnUpdate(aData[i], nRow, i, false);
            }

            oTable.fnDraw();
        }

        function editRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);

            var formName = oTable[0].className.substr(0, oTable[0].className.indexOf(' '));

            //********************* Appraisal Objective ***************************
            if (formName == "appraisalObjectives")
            {
                jqTds[0].innerHTML = '<select id="dpCategory" class="form-control input-small"></select>';
                jqTds[1].innerHTML = '<textarea rows="3"  maxlength="500"  class="form-control input custom-maxlength">' + aData[1] + '</textarea>'; //////////////////Addedby:Astha
                jqTds[2].innerHTML = '<select id="dpType" class="form-control input-small"><option value="">Select Type</option><option value="Rating5">Rating 1-5</option>'
                         + '<option value="Rating10">Rating 1-10</option>'
                         + '<option value="Percentage">Percentage</option>'
                         + '<option value="YESorNO">YES or NO</option>'
                         + '<option value="PFGVE">PFGVE</option></select>';
                jqTds[3].innerHTML = '<input type="hidden" value="' + aData[3] + '"/>'; 
                jqTds[3].hidden = 'true';//////////////////Addedby:Astha
                jqTds[4].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[4].className = 'text-center';
                jqTds[5].innerHTML = '<a class="cancel" href="">Cancel</a>';
                jqTds[5].className = 'text-center';
                if (aData[2] == "Rating 1-5" || aData[2] == "Rating 1-10") {
                    var sval = aData[2] == "Rating 1-5" ? "Rating5" : "Rating10";
                    $(dpType).val(sval);
                }
                else if(aData[2] == "YES or NO")
                {
                    $(dpType).val("YESorNO");
                }
                else {
                    $(dpType).val(aData[2]);
                }
                
                /////////////Populating dropdown for Category   //////////////////Addedby:Astha
                $.ajax({
                    type: "POST",
                    url: "AppraisalObjectives.aspx/GetCategories",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var selectedval, obj = JSON.parse(response.d);
                        var optionsHTML = [];
                        optionsHTML.push('<option value="">Select Category</option>');

                        for (var i = 0; i < obj.datatb.length; i++) {
                            optionsHTML.push('<option value="' + obj.datatb[i].Id + '">' + obj.datatb[i].CategoryName + '</option>');
                            if(obj.datatb[i].CategoryName== aData[0])
                            {
                                selectedval = obj.datatb[i].Id;
                            }
                        }
                        $(dpCategory).append(optionsHTML.join('\n'));
                        $(dpCategory).val(selectedval)
                    },
                    failure: function (response) {
                        WarningNotification(response.d);
                    }
                }); 
            }
                //********************** Appraisal Templates***************************
            else if (formName == "appraisalTemplates") {
                jqTds[0].innerHTML = '<input type="text" class="form-control custom-maxlength" maxlength="100" value="' + aData[0] + '"/>';
                var dt = aData[1] == "Active" ? 1 : 0;
                jqTds[1].innerHTML = '<select id="dpIsActive" class="form-control input-small" ><option value="1">Active</option>'
                         + '<option value="0">Inactive</option></select>';
                $(dpIsActive).val(dt);
                jqTds[2].innerHTML = '<input type="hidden" value="' + aData[2] + '"/>';
                jqTds[2].hidden = 'true';//////////////////Addedby:Astha
                jqTds[3].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[3].className = 'text-center';
                jqTds[4].innerHTML = '<a class="cancel" href="">Cancel</a>';
                jqTds[4].className = 'text-center';
                jqTds[5].innerHTML = '';
                jqTds[5].className = 'text-center';
            }
                //********************* Appraisal Category***************************
            else {
                jqTds[0].innerHTML = '<input type="text" id="txtCategory"  maxlength="100" class="form-control input-small custom-maxlength" value="' + aData[0] + '">';
                jqTds[1].innerHTML = '<input type="hidden" value="' + aData[1] + '">';
                jqTds[1].hidden = 'true';  //////////////////Addedby:Astha
                jqTds[2].innerHTML = '<a class="edit" href="">Save</a>';
                jqTds[2].className = 'text-center';
                jqTds[3].innerHTML = '<a class="cancel" href="">Cancel</a>';
                jqTds[3].className = 'text-center';
                jqTds[4].hidden = 'true';
                //jqTds[4].className = 'text-center';
            }
        }

        function saveRow(oTable, nRow) {
            var formName = oTable[0].className.substr(0, oTable[0].className.indexOf(' '));
            var done = true;
            
            //********************* Appraisal Objective ***************************
            if (formName == "appraisalObjectives") {
                var jqInputs = $('select', nRow); //////////////////Addedby:Astha
                var jqtxtInputs = $('textarea', nRow); //////////////////Addedby:Astha
                var jqhdnInputs = $('input', nRow);
                if (jqInputs[0].value == "")
                {
                    WarningNotification("Category not Selected");
                    return false;
                }
                else if (jqtxtInputs[0].value == "")
                {
                    WarningNotification("Objective is not added");
                    return false;
                }
                else if (jqInputs[1].value == "") {
                    WarningNotification("Objective Response Type is not selected");
                    return false;
                }
               
                //////////////////Below Code Addedby:Astha
                var obId = jqhdnInputs[0].value != "" ? jqhdnInputs[0].value : 0;
                var objObjective = {
                    Id:obId,
                    CategoryId: jqInputs[0].value,
                    Title: jqtxtInputs[0].value,
                    RatingType: jqInputs[1].value
                };
               
                $.ajax({
                    type: "POST",
                    url: "AppraisalObjectives.aspx/SaveObjective",
                    async: false,
                    data: JSON.stringify({ objObjective }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var obj = JSON.parse(response.d);
                        if (obj > 0) {
                            oTable.fnUpdate(jqInputs[0].selectedOptions[0].innerHTML, nRow, 0, false);
                            oTable.fnUpdate(jqtxtInputs[0].value, nRow, 1, false);
                            oTable.fnUpdate(jqInputs[1].selectedOptions[0].innerHTML, nRow, 2, false);
                            //oTable.fnUpdate(jqInputs[1].selectedOptions[0].innerHTML, nRow, 2, false);
                            oTable.fnUpdate('<a class="edit" href="javascript:;"><i class="fa fa-pencil"></i></a>', nRow, 4, false);
                            oTable.fnUpdate('<a class="delete" href="javascript:;"><i class="fa fa-trash"></i></a>', nRow, 5, false);
                            oTable.fnUpdate(obj, nRow, 3, false);
                        }
                        else {
                            WarningNotification("Objective Not Saved! the objective you are trying to save already exists..");
                            done = false;
                        }


                    },
                    failure: function (response) {
                        WarningNotification(response.d);
                    }
                });
               

            }
                //********************** Appraisal Templates***************************
            else if (formName == "appraisalTemplates") {

                var jqInputs = $('select', nRow); //////////////////Addedby:Astha              
                var jqhdnInputs = $('input', nRow);
                if (jqhdnInputs[0].value == "") {
                    WarningNotification("Template Name cannot be Null");
                    return false;
                }
                
                else if (jqInputs[0].selectedOptions.length < 1) {
                    WarningNotification("Please select template is active or not");
                    return false;
                }
                var dt = jqInputs[0].value==1? "Active" : "Inactive";
                
                $.ajax({
                    type: "POST",
                    url: "AppraisalTemplate.aspx/SaveTemplate",
                    async: false,
                    data: JSON.stringify({ TempId: jqhdnInputs[1].value, TempName: jqhdnInputs[0].value, Active: jqInputs[0].value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var obj = JSON.parse(response.d);
                        if (obj > 0) {
                           
                            oTable.fnUpdate(jqhdnInputs[0].value, nRow, 0, false);
                            oTable.fnUpdate(dt, nRow, 1, false);

                            oTable.fnUpdate('<a class="edit" href="javascript:;"><i class="fa fa-pencil"></i></a>', nRow, 3, false);
                            oTable.fnUpdate('<a class="delete" href="javascript:;"><i class="fa fa-trash"></i></a>', nRow, 4, false);
                            oTable.fnUpdate('<a data-toggle="modal" data-target="#myModal"><i class="fa fa-plus"></i></a>', nRow, 5, false);
                            oTable.fnUpdate(obj, nRow, 2, false);
                        }
                        else {
                            WarningNotification("Template Not Saved! the Template Name you are trying to save already exists..");
                            done = false;
                        }


                    },
                    failure: function (response) {
                        WarningNotification(response.d);
                    }
                });

            }
                //********************* Appraisal Category***************************
            else {
                var jqInputs = $('input', nRow);
                if (jqInputs[0].value == "") {
                    WarningNotification("Category name cannot be empty");
                    return false;
                }
               


                $.ajax({
                    type: "POST",
                    url: "AppraisalCategory.aspx/SaveCategory",
                    async: false,
                    data: JSON.stringify({ CatId: jqInputs[1].value, catName: jqInputs[0].value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var obj = JSON.parse(response.d);
                        if (obj > 0) {
                        var obj = JSON.parse(response.d);
                        oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
                        oTable.fnUpdate('<a class="edit" href="javascript:;"><i class="fa fa-pencil"></i></a>', nRow, 2, false);
                        oTable.fnUpdate('<a class="delete" href="javascript:;"><i class="fa fa-trash"></i></a>', nRow, 3, false);
                        oTable.fnUpdate('', nRow, 4, false);
                        oTable.fnUpdate(obj, nRow, 1, false);
                        }
                        else {
                            WarningNotification("Category Not Saved! the Category Name you are trying to save already exists..");
                            done = false;
                        }
                    },
                    failure: function (response) {
                        WarningNotification(response.d);
                    }
                });
            }
            oTable.fnDraw();
            return done;
        }




        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            //oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            //oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            //oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 1, false);
            oTable.fnDraw();
        }

        var table = $('#sample_editable_1');

        var oTable = table.dataTable({

            // Uncomment below line("dom" parameter) to fix the dropdown overflow issue in the datatable cells. The default datatable layout
            // setup uses scrollable div(table-scrollable) with overflow:auto to enable vertical scroll(see: assets/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js). 
            // So when dropdowns used the scrollable div should be removed. 
            //"dom": "<'row'<'col-md-6 col-sm-12'l><'col-md-6 col-sm-12'f>r>t<'row'<'col-md-5 col-sm-12'i><'col-md-7 col-sm-12'p>>",

            "lengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"] // change per page values here
            ],

            // Or you can use remote translation file
            //"language": {
            //   url: '//cdn.datatables.net/plug-ins/3cfcc339e89/i18n/Portuguese.json'
            //},

            // set the initial value
            "pageLength": 5,

            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [{ // set default column settings
                'orderable': true,
                'targets': [0]
            }, {
                "searchable": false,
                "targets": [target]
            }],
            "order": [
                [0, "asc"]
            ] // set first column as a default sort by asc
        });

        var tableWrapper = $("#sample_editable_1_wrapper");

        var nEditing = null;
        var nNew = false;
        
        $('#sample_editable_1_new').click(function (e) {
            e.preventDefault();

            if (nNew && nEditing) {
                event.preventDefault();
                return;
                //if (confirm("Previous row not saved. Do you want to save it ?")) {
                //    saveRow(oTable, nEditing); // save
                //    $(nEditing).find("td:first").html("Untitled");
                //    nEditing = null;
                //    nNew = false;

                //} else {
                //    oTable.fnDeleteRow(nEditing); // cancel
                //    nEditing = null;
                //    nNew = false;
                    
                //    return;
                //}
            }

            var aiNew = oTable.fnAddData(['', '', '', '', '', '']);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            isupdate = false; //////////////////Addedby:Astha
            nEditing = nRow;
            nNew = true;
        });



        table.on('click', '.delete', function (e) {
            e.preventDefault();

            //if (confirm("Are you sure to delete this record ?") == false) {
            //    return;
            //}
            var nRow = $(this).parents('tr')[0];
            var aData = oTable.fnGetData(nRow);
            var DataId = 0, deleteUrl = "", deletecnfrmstr = "";
            var formName = oTable[0].className.substr(0, oTable[0].className.indexOf(' ')); //////////////////Addedby:Astha
            if (formName == "appraisalObjectives") {
               
                DataId = aData[3];
                deleteUrl = "AppraisalObjectives.aspx/DeleteObjective";
                deletecnfrmstr = "Objective";
            }
            else if (formName == "appraisalTemplates") {
                DataId = aData[2];
                deleteUrl = "AppraisalTemplate.aspx/DeleteTemplate";
                deletecnfrmstr = "Template";
            }
            else {
                DataId = aData[1];
                deleteUrl = "AppraisalCategory.aspx/DeleteCategory";
                deletecnfrmstr = "Category";
            }
            if (GetConfirmationAppraisal("Are you sure to delete this " + deletecnfrmstr + " ?", DataId, deletecnfrmstr, deleteUrl,oTable,nRow) == false) {
                return;
            }
            ////////////////// Code below Addedby:Astha
            
           
            
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nEditing = null;
                nNew = false;
               
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {
            e.preventDefault();
            
            nNew = false;
            
            
            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                var v = saveRow(oTable, nEditing);                
               
                if (v != false) {
                    nEditing = null;
                    SuccessNotification("Record saved Successfully. ");
                   // alert("Successful Update!! Record has been saved...");
                }
            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });
    }

    return {

        //main function to initiate the module
        init: function (target) {
            handleTable(target);
        }

    };

}();
function deleteRecord(deletecnfrmstr, DataId, deleteUrl, oTable, nRow)
{

    $.ajax({
        type: "POST",
        url: deleteUrl,
        async: false,
        data: JSON.stringify({ Id: DataId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var rply = response.d;
            if (rply == "false") {
                WarningNotification("Can not Delete!! This " + deletecnfrmstr + " has been used");
            }
            else {
                oTable.fnDeleteRow(nRow);
                SuccessNotification("Deleted!! " + deletecnfrmstr + " has been deleted");
            }


        },
        failure: function (response) {
            WarningNotification(response.d);
        }
    });
}
function GetConfirmationAppraisal(_msg, DataId, deletecnfrmstr, deleteUrl, oTable, nRow) {

     event.preventDefault();
    
     var   _caption = "Confirmation";
    //if (!_targetclick)
    //    _targetclick = "delete";
   
     var   _btn1 = "Delete";
 
     var   _btn2 = "Cancel";
    var _dialog = window.confirm = function (message, callback, caption) {
        caption = _caption
        $(document.createElement('div')).attr({
            title: _caption,
            'class': 'dialog jscustom-dialog'
        }).html(message).dialog({
            position: ['center', 100],
            dialogClass: 'fixed',
            buttons: {
                "[_btn1]": {
                    text: [_btn1],
                    'class': 'btn btn-primary',
                    click: function () {
                        //$('#' + _targetclick).click();
                        $(this).dialog('close');
                        deleteRecord(deletecnfrmstr, DataId, deleteUrl, oTable, nRow);
                    }
                },
                "[_btn2]": {
                    text: [_btn2],
                    'class': 'btn btn-danger',
                    click: function () {
                        $(this).dialog('close');
                        return false;
                    }
                }



                //[_btn1]: function () {
                //    $('#' + _targetclick).click();
                //    $(this).dialog('close');
                //                        //return true;
                //},
                //[_btn2]: function () {
                //    $(this).dialog('close');
                //    //$(_this).prop('checked', false);
                //    return false;
                //},

            },
            close: function () {
                $(this).remove();
                //$(_this).prop('checked', false);
                return false;
            },
            draggable: true,
            modal: true,
            resizable: false,
            width: 'auto'
        });
    };
    confirm(_msg, function () {
        console.log('confirmed')
    })
}

jQuery(document).ready(function () {
    var target = 0;
    var frmname = $("#sample_editable_1").attr('class').substr(0, $("#sample_editable_1").attr('class').indexOf(' '));
    if (frmname == "appraisalObjectives") {
        target = 3;
        
    }
    else if (frmname == "appraisalTemplates") {
        target = 2;
    }
    else {
        target = 1;
    }
   

    TableDatatablesEditable.init(target);
});

