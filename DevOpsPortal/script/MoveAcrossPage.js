$(document).ready(function (e) {
    var width = $(document).width();

    function goRight() {
        $("#animate").animate({
            left: width - 600
        }, 4000, function () {
            setTimeout(goLeft, 50);
        });
    }
    function goLeft() {
        $("#animate").animate({
            left: 0
        }, 4000, function () {
            setTimeout(goRight, 50);
        });
    }

    $("#animate").animate({
        left: 600
    }, 0, function () {

    });
    setTimeout(goLeft, 50);
});