var _formId = "";
$(document).ready(function () {
    _formId = $('form').attr('id');
    SuccessNotification();
    $('#errorContainer .closeIcon,#successFixedContainer .closeIcon').click(function () {
        $('#errorContainer').remove();
    });


    $('form').on('submit.validate', function () {
        $(this).attr('showErrorLabelOnfocus', true)
        //$('label.error').addClass('hidden');
    });

    $(document).on('click', '.close-notification', function () {
        $('#successContainer,#dangerAlert,#infoAlert,#successFixedContainer').remove();
    });

    $('.RadMenu_Context > ul > li').addClass('hidden');
    $('.RadMenu_Context ul').find('.rmLast').removeClass('hidden');
});

function SuccessNotification(_customMessages, _form) {
    $('#successContainer').hide(1000);
    $('#dangerAlert').hide(1000);
    if (!_form) _form = _formId;
    var successCorntainer =
 ['<div id="successContainer">',
 //'<img  src="' + GetBaseURL() + 'images/NotificationInfo.png" />',
 '<div><i class="fa fa-check" aria-hidden="true"></i>',
   '<span>has been saved.</span>',
    '</div>',
   '</div>'
 ].join('\n');

    var successMessage = $(document).find('#ResponseSuccessMessage').val();
    if (!successMessage)
        successMessage = _customMessages;
    if (!successMessage)
        return true;
    $('#errorContainer').hide();
    if ($('#successContainer').length == 0) {
        //if (_form)
        //    $('#' + _form + ' *:first').before(successCorntainer);
        //else
        $('.page-content,custom-page-content').before(successCorntainer);
    }
    //$('#successContainer').fadeIn(1000);
    //$('#successContainer span').html(successMessage);
    //$('#successContainer').fadeOut(8000);


    //$('#successContainer').show(1000);
    $('#successContainer span').html(successMessage);

    $('#successContainer').slideDown("slow");

    setTimeout(function () {
        $('#successContainer').slideUp("slow");
    }, 5000);

    //$('#successContainer').fadeOut(8000);


    $(document).find('#ResponseSuccessMessage').val('');

};


$(document).on('click', '.hideErrorContainer', function () {
    $('#errorContainer').fadeOut(1000);
});

function showWaitContainer(_targetElement, waitMsg) {
    if (!waitMsg)
        waitMsg = "Loading";
    var waitcontainer =
    ['<div id="waitdiv">',
    '<p class="inlineBlock"></p>',
    '<img class="inlineBlock" src="' + GetBaseURL() + 'images/ajax-loader.gif"/>',
    '</div>'
    ].join('\n');


    $('#' + _targetElement).append(waitcontainer);
    if ($('#' + _targetElement).length == 0)
        $('.' + _targetElement).append(waitcontainer);
    if (waitMsg == "0") {
        $('#waitdiv').css('display', 'none');
    } else {
        $('#waitdiv').css('display', 'block');
    }
    $('#waitdiv p').html(waitMsg);
    $('#waitdiv').css("top", ($('#' + _targetElement).height() / 2) - ($('#waitdiv').outerHeight() / 2));
    $('#waitdiv').css("left", ($('#' + _targetElement).width() / 2) - ($('#waitdiv').outerWidth() / 2));

    if ($('#' + _targetElement).length == 0) {
        $('#waitdiv').css("top", ($('.' + _targetElement).height() / 2) - ($('#waitdiv').outerHeight() / 2));
        $('#waitdiv').css("left", ($('.' + _targetElement).width() / 2) - ($('#waitdiv').outerWidth() / 2));
    }

}


function WarningNotification(_customMessages, _form) {
    $('#dangerAlert').hide(1000);
    if (!_form) _form = _formId;
    var dangerAlert =
 ['<div id="dangerAlert">',
 //'<img  src="' + GetBaseURL() + 'images/NotificationInfo.png" />',
 '<div class="messagebox">',//<i class="fa fa-warning" aria-hidden="true"></i>',
   '<span>has been saved.</span>',
    '</div>',
    '<i class="fa fa-remove close-notification" aria-hidden="true"></i>',
   '</div>'
 ].join('\n');

    var successMessage = $(document).find('#ResponseSuccessMessage').val();
    if (!successMessage)
        successMessage = _customMessages;
    if (!successMessage)
        return true;
    $('#errorContainer').hide();
    if ($('#dangerAlert').length == 0) {
        $('.page-content,.custom-page-content').before(dangerAlert);
    }
    $('#dangerAlert span').html(successMessage);

    $('#dangerAlert').slideDown("slow");
    return false;
    //setTimeout(function () {
    //    $('#dangerAlert').slideUp("slow");
    //}, 1400);
}

function SuccessFixedNotification(_customMessages, _form) {
    $('#successFixedContainer').hide(1000);
    if (!_form) _form = _formId;
    var dangerAlert =
 ['<div id="successFixedContainer">',

 '<div class="messagebox">',
   '<span>has been saved.</span>',
    '</div>',
    '<i class="fa fa-remove close-notification" aria-hidden="true"></i>',
   '</div>'
 ].join('\n');

    var successMessage = $(document).find('#ResponseSuccessMessage').val();
    if (!successMessage)
        successMessage = _customMessages;
    if (!successMessage)
        return true;
    $('#errorContainer').hide();
    if ($('#successFixedContainer').length == 0) {
        $('.page-content,.custom-page-content').before(dangerAlert);
    }
    $('#successFixedContainer span').html(successMessage);

    $('#successFixedContainer').slideDown("slow");
    return false;
}



function CallNotification(_message)
{
    if (_message.split('|')[0] === 'Warning')
        WarningNotification(_message.split('|')[1]);
    else if (_message.split('|')[0] === 'Fixed')
        SuccessFixedNotification(_message.split('|')[1]);
    else
        SuccessNotification(_message.split('|')[1]);
    //_message = _message.split('|')[0] == "Warning" ? WarningNotification(_message.split('|')[1]) : SuccessNotification(_message.split('|')[1]);
}



function InfoNotification(_customMessages, _form) {
    $('#infoAlert').hide(1000);
    if (!_form) _form = _formId;
    var infoAlert =
 ['<div id="infoAlert">',
 //'<img  src="' + GetBaseURL() + 'images/NotificationInfo.png" />',
 '<div class="messagebox">',//<i class="fa fa-warning" aria-hidden="true"></i>',
   '<span>has been saved.</span>',
    '</div>',
    '<i class="fa fa-remove close-notification" aria-hidden="true"></i>',
   '</div>'
 ].join('\n');

    var successMessage = $(document).find('#ResponseSuccessMessage').val();
    if (!successMessage)
        successMessage = _customMessages;
    if (!successMessage)
        return true;
    $('#errorContainer').hide();
    if ($('#infoAlert').length == 0) {
        $('.page-content,.custom-page-content').before(infoAlert);
    }
    $('#infoAlert span').html(successMessage);

    $('#infoAlert').slideDown("slow");
    return false;
    //setTimeout(function () {
    //    $('#infoAlert').slideUp("slow");
    //}, 1400);
}

function GetConfirmation(_msg, _targetclick, _caption, _btn1, _btn2) {
    event.preventDefault();
    if (!_caption)
        _caption = "Confirmation";
    if (!_btn1)
        _btn1 = "Submit";
    if (!_btn2)
        _btn2 = "Cancel";
    var _dialog = window.confirm = function (message, callback, caption) {
        caption =  _caption
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
                        $('#' + _targetclick).click();
                        $(this).dialog('close');
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
