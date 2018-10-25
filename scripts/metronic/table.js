$(document).ready(function () {  

    var radMenu = document.createElement("script");
    radMenu.src = "../scripts/radMenu.js";
    radMenu.type = "text/javascript";
    document.getElementsByTagName("head")[0].appendChild(radMenu);

    $(window).load(function () {
        $("#RadSplitter1, #RAD_SPLITTER_PANE_CONTENT_gridPane2,#RadGrid1_GridData,.rgDataDiv").css("height", "");
        //$("#RadSplitter1, #RAD_SPLITTER_PANE_CONTENT_gridPane2,#RadGrid1_GridData,.rgDataDiv").css("width", "");


        //var distanceTop = $('table.rgMasterTable:first-of-type').offset().top;
        //var documentHeight = $(window).height();
        //var maxHeight = (documentHeight - distanceTop) - 265;
        //var maxHeight = (documentHeight - distanceTop) - 77;
        //var maxHeight_2 = (documentHeight - distanceTop) - 77;
        //if (maxHeight < 170) maxHeight = 170;
        //if (maxHeight_2 < 170) maxHeight_2 = 170;

        //$("#RadGrid1_GridData, .rgDataDiv").css("maxHeight", maxHeight);
        //$(".radGrid-single").css({ "maxHeight": maxHeight_2, "overflow": "auto" });
        
        var documentHeight = $(window).height() - 250;
        $("#RadGrid1_GridData, .rgDataDiv").css("maxHeight", documentHeight);
        $(".radGrid-single").css({ "maxHeight": documentHeight, "overflow": "auto" });

        checkScroll();

        //$.fn.hasScrollBar = function () {
        //    return this.get(0).scrollHeight > this.height();
        //}

        //if ($('.rgDataDiv').hasScrollBar()) {
        //    $(".rgDataDiv").prev().addClass("scroll");
        //}
        //else {
        //    $(".rgDataDiv").prev().removeClass("scroll");
        //}

        //if ($('#RadGrid2_GridData').hasScrollBar()) {
        //    $("#RadGrid2_GridData").prev().addClass("scroll");
        //}
        //else {
        //    $("#RadGrid2_GridData").prev().removeClass("scroll");
        //}


    });
});

$(".check-scroll").click(function () {
    $("#tbsuser").css({"display":"block"});
    checkScroll();
});


function checkScroll (){

    $.fn.hasScrollBar = function () {
        return this.get(0).scrollHeight > this.height();
    }

    $("div.rgDataDiv").each(function () {
        if ($(this).hasScrollBar()) {
            $(this).prev().addClass("scroll");
        }
        else {
            $(this).prev().removeClass("scroll");
        }
    });


}