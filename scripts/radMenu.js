//Setting Filter Options Position
var topDistance = 0;
var leftDistance = 0;
var rightDistance = 0;
var TagWidth = 0;
$(".rgFilter").click(function () {
    topDistance = $(this).offset().top - $("form").offset().top;
    leftDistance = $(this).offset().left - $("form").offset().left + 38;
    TagWidth = $(".RadMenu").width();
    rightDistance = $(window).width() - $(this).offset().left;
    if (TagWidth > rightDistance) {
        leftDistance = leftDistance - TagWidth - 26;
    }
    $(".RadMenu").css("top", topDistance);
    $(".RadMenu").css("left", leftDistance);
});

$(".rgMasterTable th").contextmenu(function () {
    topDistance = $(this).offset().top - $("form").offset().top+1;
    leftDistance = $(this).offset().left - $("form").offset().left + $(this).width() + 30;

    TagWidth = $(".RadMenu").width();
    rightDistance = $(window).width() - $(this).offset().left - $(this).width();
    if (TagWidth > rightDistance) {
        leftDistance = leftDistance - TagWidth - $(this).width() - 5;
    }

    $(".RadMenu").css("top", topDistance);
    $(".RadMenu").css("left", leftDistance);
});