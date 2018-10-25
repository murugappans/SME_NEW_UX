$(document).ready(function () {
    $(document).on('keypress', '.numericonly', function (e) {
        return numbersonly(e);
    });
    $(document).on('keypress', '.custom-maxlength', function (e) {
        return maxlength(this);
    });
    $(document).on('keypress', '.number-dot', function (e) {
        return numberswithdot(e);
    });

    $(document).on('keypress', '.alphabetsonly', function (e) {
        return alphabetssonly(e);
    });

    //$(document).on('keydown change', '.alphabetsonly', function (e) {
    //    return setAlbhabetString($(this));
    //});
    //$(document).on('change ', '.alphabetsonly', function (e) {
    //    return setAlbhabetString($(this));
    //});

    $(document).on('keypress', '.cleanstring', function (e) {
        return cleanString(e);
    });

    $('#btnSearchMoreCity').click(function (e) {
        var _searchval = $.trim($('#morecity').val());
        if (_searchval === "") {
            $('#morecity').val("");
            $('#formMoreCity').valid();
            return false;
        }
        var _url = getAppURL() + "Car/CarListByCity?city=" + $('#morecity').val();
        var _href = "location.href='" + _url + "'";
        $('.lnkCustomCity').attr('onclick', encodeURI(_href));
        $('.lnkCustomCity').click();
    });
    $('#morecity').keypress(function (event) {
        if (event.keyCode == 13) {
            $('#btnSearchMoreCity').click();
        }
    })

    $(document).on('click', '.close-notification', function () {
        _showmessage = false;
    });

    $(document).on('focusout keydown change', '.custom-maxlength', function () {
        return setCharacterLength($(this));

    });
    $(document).on('focusout keydown change', '.numericonly', function () {
        return setNumericString($(this));

    });
    $(document).on('focusout', '.number-dot', function () {
        return setNumberDotString($(this));

    });
    $(document).on('focusout keydown change', '.alphabetsonly', function () {
        return setAlbhabetString($(this));

    });
    $(document).on('focusout keydown change', '.cleanstring', function () {
        return setCleanString($(this));

    });
});

function getRandomNumber(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

// Adding extend function to Object.prototype
function extendObject(_obj) {
    Object.prototype.extend = function (_obj) {
        for (var i in _obj) {
            if (_obj.hasOwnProperty(i)) {
                this[i] = _obj[i];
            }
        }
    };
}
// Using try-catch to handle the JSON.parse error
function parse(str) {
    try {
        return JSON.parse(str);
    }

    catch (e) {
        return false;
    }
}

function emptyGuid() {
    return "00000000-0000-0000-0000-000000000000"
}


function getObjectByProperty(_array, _property) {
    var _retArray = new Array();
    var result = _array.filter(function (Object) {
        if (Object.OemName === _property) {
            _retArray = Object;
        }
    });
}


function numbersonly(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
        if (unicode < 48 || unicode > 57) //if not a number
            return false //disable key press

        return maxlength($(e.target));

    }
}
var _showmessage = false;
function numberswithdot(e) {
    var unicode = e.charCode ? e.charCode : e.keyCode
    var element = $(e.target);
    var _id = $(element).attr('id');
    var txt = document.getElementById(_id);
    //var _maxlength = $(element).attr('MaxLength');
    if ($.trim($(element).val()) == "" && unicode == 46) {
        $(element).val("");
        return false;
    }

    if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
        if (unicode != 46 && (unicode < 48 || unicode > 57)) //if not a number
            return false //disable key press
        if ($(element).val().split('.').length >= 2 && unicode == 46)
            return false

        if ($(element).val().split('.')[1] && $(element).val().split('.')[1].length >= 2) {
            if ((txt.value.split('.')[1].substr(txt.selectionStart, (txt.selectionEnd - txt.selectionStart)).length == 2)) {
                return true;
            }
            else{
            if (_showmessage == false)
                WarningNotification('Only two decimals are allowed(00.00)');
            _showmessage = true;
            return false
            }
        }
        //if ($(element).val().length >= _maxlength) {
        //    if (_showmessage == false)
        //        WarningNotification('Only ' + _maxlength + ' characters and two decimals are alowed');
        //    _showmessage = true;
        //    return false;
        //}

        return maxlength(element);


    }
}

function maxlength(_elem) {
    var _id = $(_elem).attr('id');
    var txt = document.getElementById(_id);
    var _maxlength = $(_elem).attr('MaxLength') ? $(_elem).attr('MaxLength') : $(_elem).data('maxlength');
    if ($(_elem).val().length >= _maxlength) {
        if (($(_elem).val().substr($(_elem).selectionStart, ($(_elem).selectionEnd - $(_elem).selectionStart)).length == _maxlength)) {
            return true;
        }
        else {
            var _type = $(_elem).data('type');

            var _message = 'Only ' + _maxlength + ' characters are allowed';
            if (_type === "currency")
                _message = 'Only ' + _maxlength + ' characters and two decimals are allowed';

            if (_showmessage == false)
                WarningNotification(_message);
            _showmessage = true;
            return false;
        }
    }

    return true;
}

function setCleanString(e) {

    var _val = $(e).val();
    var re = /[^a-zA-Z0-9 ]/g;
    if (_val != "" && re.test(_val)) {
        WarningNotification("Invalid Input! No special characters are allowed..");
        $(e).val("");
        return false;
    }


}
function setAlbhabetString(e) {
   
    var  _val = $(e).val();
    var re = /^[a-zA-Z ]+$/;
    if (_val != "" && !re.test(_val)) {
        event.preventDefault();
        WarningNotification("Invalid Input! Only alphabets are allowed..");
        $(e).val("");
        return false;
    }
       
  
}
function setNumberDotString(e) {

    var _val = $(e).val();
    var re = /^\d+(\.\d{1,2})?$/;
    if (_val != "" && !re.test(_val)) {
        WarningNotification("Invalid Input! Only  two decimals numbers are allowed..");
        $(e).val("");
        return false;
    }


}
function setNumericString(e) {

    var _val = $(e).val();
    var re = /^[0-9]+$/;
    if (_val != "" && !re.test(_val)) {
        WarningNotification("Invalid Input! Only numbers are allowed..");
        $(e).val("");
        return false;
    }


}
function alphabetssonly(e) {
    var element = $(e.target);
    var unicode = e.charCode ? e.charCode : e.keyCode
    if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
        if (!(unicode >= 65 && unicode <= 122) && (unicode != 32 && unicode != 0))
            return false //disable key press
       
           
    }

    return maxlength(element);
}


function cleanString(e) {
    var element = $(e.target);
    var unicode = e.charCode ? e.charCode : e.keyCode;
    if (unicode == 35 || unicode == 42 || unicode == 47 || unicode == 43 || unicode == 64 || unicode == 63 || unicode == 92 || unicode == 124 || unicode == 33 || unicode == 36 || unicode == 37 || unicode == 94 || unicode == 38 || unicode == 40 || unicode == 41 || unicode == 31 || unicode == 61 || unicode == 96 || unicode == 126 || unicode == 58 || unicode == 59 || unicode == 34 || unicode == 39 || unicode == 60 || unicode == 62 || unicode == 44 || unicode == 123 || unicode == 125 || unicode == 45 || unicode == 91 || unicode == 93 || unicode == 95 || unicode == 46)
        return false;
}




function setCharacterLength(_this) {
    var _maxlength = $(_this).data('maxlength');
    if ($(_this).val().length >= _maxlength) {
        var _currentval = _this.val().length;
        if (_currentval > _maxlength) {
            var _val = _this.val();
            var res = _val.slice(0, _maxlength);
            _this.val(res);
            WarningNotification('Only ' + _maxlength + ' characters are allowed');
        }
    }
  
}
