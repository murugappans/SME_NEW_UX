var _formId = "";
$(document).ready(function () {
    _formId = $('form').attr('id');
    SuccessNotification();
    $('#errorContainer .closeIcon').click(function () {
        $('#errorContainer').remove();
    });


    $('form').on('submit.validate', function () {
        $(this).attr('showErrorLabelOnfocus', true)
        //$('label.error').addClass('hidden');
    });

    $(document).on('click', '.close-notification', function () {
        $('#successContainer,#dangerAlert,#infoAlert').remove();
    });
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
            $('.page-content').before(successCorntainer);
    }
    //$('#successContainer').fadeIn(1000);
    //$('#successContainer span').html(successMessage);
    //$('#successContainer').fadeOut(8000);


    //$('#successContainer').show(1000);
    $('#successContainer span').html(successMessage);

    $('#successContainer').slideDown("slow");

    setTimeout(function () {
        $('#successContainer').slideUp("slow");
    }, 2500);

    //$('#successContainer').fadeOut(8000);


    $(document).find('#ResponseSuccessMessage').val('');

};
function WarningNotification2(_customMessages, _form) {
    if (!_form) _form = _formId;
    var dangerAlert =
    ['<div class="alert alert-danger display-none" id="dangerAlert">',
      '<button class="close" data-dismiss="alert"></button>',
      '<span> </span>',
       '</div>'
    ].join('\n');
    
    var successMessage = $(document).find('#ResponseSuccessMessage').val();
    if (!successMessage)
        successMessage = _customMessages;
    if (!successMessage)
        return true;
    $('#dangerAlert').hide();
    if ($('#dangerAlert').length == 0) {
        if (_form)
            $('#' + _form + ' *:first').before(dangerAlert);
        else
            $('.page-content').before(dangerAlert);
    }
    $('#dangerAlert').show();
    $('#dangerAlert span').html(successMessage);
    $(document).find('#ResponseSuccessMessage').val('');

    $('.modal-backdrop').hide();
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

function CallNotification(_message)
{
   
    _message = _message.split('|')[0] == "Warning" ? WarningNotification(_message.split('|')[1]) : SuccessNotification(_message.split('|')[1]);
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