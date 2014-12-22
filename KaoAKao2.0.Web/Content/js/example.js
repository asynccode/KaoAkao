/*
 *  导航JS
 */
$(function(){

    var $el, leftPos, newWidth,
        $mainNav = $("#example"),
        $mainNav2 = $("#example-two");
    
    $mainNav.append("<div id='magic'></div>");
    
    var $magicLine = $("#magic");
    
    $magicLine
        .width($(".current").width())
        .css("left", $(".current a").position().left)
        .data("origLeft", $magicLine.position().left)
        .data("origWidth", $magicLine.width());
        
    $("#example li").find("a").hover(function() {
        $el = $(this);
        leftPos = $el.position().left;
        newWidth = $el.parent().width();
        
        $magicLine.stop().animate({
            left: leftPos,
            width: newWidth
        });
    }, function() {
        $magicLine.stop().animate({
            left: $magicLine.data("origLeft"),
            width: $magicLine.data("origWidth")
        });    
    });
});