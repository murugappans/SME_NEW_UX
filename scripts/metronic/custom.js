
var horMenuwidth = 0;


$(".menu-toggler.sidebar-toggler").click(function () {
    if ($("body.page-sidebar-closed").length > 0) {
        localStorage.setItem('sidebarLocked', true);
    }
    else {
        localStorage.setItem('sidebarLocked', false);
    }
});


//Page Loader Start
$(document).on('click', ".nav .dropdown-menu.pull-left a:not(.target-menu), .page-sidebar .nav-item:not(.nav-item-theme)  .nav-item:not(.target-menu)", function (event) {
    if (event) {
        var ctrl = event.ctrlKey;
        var shift = event.shiftKey;
    }
    else {
        var ctrl = window.event.ctrlKey;
        var shift = window.event.shiftKey;
    }
    if (ctrl == false & shift == false) {
        $(".loader").css("display", "block");
    }
});

//$(window).bind('beforeunload', function () {    
//    $(".loader").css("display", "block");
//});

$(window).load(function () {

    if (localStorage.getItem("sidebarLocked") !== null) {
        if (localStorage.getItem("sidebarLocked") == "true") {
            //alert("true");
            //$('.menu-toggler.sidebar-toggler').trigger('click');

            $('.page-sidebar > ul').removeClass("page-sidebar-menu-closed");
            $('body').removeClass("page-sidebar-closed");

        }
    }
    
    horMenuwidth = $(".page-header .page-header-inner .hor-menu").width();
    checkMenuoverflow()

    $('body .page-header, body .page-container, body .page-footer').css("visibility", "visible");
    $('body').css("background-color", "#364150");

    $(".loader").fadeOut();

});

//Page Loader End



$(window).resize(function () {
    checkMenuoverflow()
});

$(document).ready(function () {

    




    //$("#RadSplitter1, #RAD_SPLITTER_PANE_CONTENT_Radpane1").removeAttr('style');


    //alert(b);

    //calander icon text remove
    $(".rcCalPopup").text('');
    //calander icon text remove

    if (localStorage.getItem("newUrl") !== null) {
        $("#style_color").attr("href", localStorage.getItem('newUrl'));
    }
    $(".tooltips").click(function () {
        localStorage.setItem('newUrl', "../Style/metronic/themes/" + $(this).attr('data-style') + ".min.css");
    });


    


    $(".rgPager .rgPagerCell .rgPagerButton").removeClass("rgPagerButton").addClass("btn btn-sm red");
    $(".search-box .form-group .RadPicker table td").removeAttr("style")


    var heightLeft = $(window).height() - $("div.login-start").height();
    $("div.login-start").css({ "padding-top": heightLeft / 2 })

    //Theme change toggler call function
    $("#themeToggler").click(function () {
        $(".theme-options, .toggler-close").css('display', 'block');
    });

    $("table.rgMasterTable").attr('Rule', 'all')
    $("table.rgMasterTable").attr('border', '1')



    // Replacing Edit and Delete Icon
    if (navigator.userAgent.search("Chrome") >= 0) {
        $("input[title='Edit']").attr({ 'alt': ' ', 'class': 'fa edit-icon edit-icon-chrome font-red', 'src': ' ' });
        $("input[title='Delete']").attr({ 'alt': ' ', 'class': 'fa delete-icon edit-icon-chrome font-red', 'src': ' ' });
        $("input[title='Delete Parameter']").attr({ 'alt': ' ', 'class': 'fa delete-icon edit-icon-chrome font-red', 'src': ' ' });
        $("input[title='Delete Item']").attr({ 'alt': ' ', 'class': 'fa delete-icon edit-icon-chrome font-red', 'src': ' ' });
    }
    else {
        $("input[title='Edit']").attr({ 'alt': ' ', 'class': 'fa edit-icon font-red', 'src': ' ' });
        $("input[title='Delete']").attr({ 'alt': ' ', 'class': 'fa delete-icon font-red', 'src': ' ' });
        $("input[title='Delete Parameter']").attr({ 'alt': ' ', 'class': 'fa delete-icon font-red', 'src': ' ' });
        $("input[title='Delete Item']").attr({ 'alt': ' ', 'class': 'fa delete-icon font-red', 'src': ' ' });
    }


    // Metronic checkbox Control
    $('#chkWF').find('td').each(function (i, el) {
        $("#chkWF_" + i + ",#chkWF_" + i + "+label").wrapAll("<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'></label>");
        $("#chkWF_" + i + "+label").after("<span></span>");
    })
    $('#chkBoxTs').find('td').each(function (i, el) {
        $("#chkBoxTs_" + i + ",#chkBoxTs_" + i + "+label").wrapAll("<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'></label>");
        $("#chkBoxTs_" + i + "+label").after("<span></span>");
    })
    $('#chkFiFo').find('td').each(function (i, el) {
        $("#chkFiFo_" + i + ",#chkFiFo_" + i + "+label").wrapAll("<label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'></label>");
        $("#chkFiFo_" + i + "+label").after("<span></span>");
    })


    // Metronic Radio Control
    $('#radListPayrollApp').find('td').each(function (i, el) {
        $("#radListPayrollApp_" + i + ",#radListPayrollApp_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#radListPayrollApp_" + i + "+label").after("<span></span>");
    })
    $('#radListLeaveApp').find('td').each(function (i, el) {
        $("#radListLeaveApp_" + i + ",#radListLeaveApp_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#radListLeaveApp_" + i + "+label").after("<span></span>");
    })
    $('#radListClaimApp').find('td').each(function (i, el) {
        $("#radListClaimApp_" + i + ",#radListClaimApp_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#radListClaimApp_" + i + "+label").after("<span></span>");
    })
    $('#radListTSApp').find('td').each(function (i, el) {
        $("#radListTSApp_" + i + ",#radListTSApp_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#radListTSApp_" + i + "+label").after("<span></span>");
    })
    $('#radListALAp').find('td').each(function (i, el) {
        $("#radListALAp_" + i + ",#radListALAp_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#radListALAp_" + i + "+label").after("<span></span>");
    })
    $('#rdWorkFlow').find('td').each(function (i, el) {
        $("#rdWorkFlow_" + i + ",#rdWorkFlow_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#rdWorkFlow_" + i + "+label").after("<span></span>");
    })
    $('#rdMultiCurr').find('td').each(function (i, el) {
        $("#rdMultiCurr_" + i + ",#rdMultiCurr_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#rdMultiCurr_" + i + "+label").after("<span></span>");
    })
    $('#rdbGrouping').find('td').each(function (i, el) {
        $("#rdbGrouping_" + i + ",#rdbGrouping_" + i + "+label").wrapAll("<label class='mt-radio mt-radio-outline'></label>");
        $("#rdbGrouping_" + i + "+label").after("<span></span>");
    })
});

function checkMenuoverflow() {
    var a = $(".page-header .page-header-inner").width();
    var b = horMenuwidth;
    var c = $(".page-header .page-header-inner .page-logo").width();
    var d = $(".page-header .page-header-inner .top-menu").width();

    $.fn.hasScrollBar = function () {
        return this.get(0).scrollHeight > this.height();
    }
    if ($('body').hasScrollBar()) {
        var e = 24;
    }
    else {
        var e = 42;
    }

    if ((b + c + d + e) > a) {
        $(".page-header .page-header-inner .hor-menu").addClass("iconOnly");
    }
    else {
        $(".page-header .page-header-inner .hor-menu").removeClass("iconOnly");
    }
}