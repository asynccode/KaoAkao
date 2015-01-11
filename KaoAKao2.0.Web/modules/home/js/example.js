/*
 *  导航JS
 */
$(function(){
    var pageIndex = $("#txt_pageIndex").val();
    if (pageIndex) {
        pageIndex = parseInt(pageIndex);
        $("#example ul li").eq(pageIndex).addClass("current");
    }
    else {
        $("#example ul li").removeClass("current");
    }

    var $el, leftPos, newWidth,
        $mainNav = $("#example"),
        $mainNav2 = $("#example-two");
    
    $mainNav.append("<div id='magic'></div>");
    
    var $magicLine = $("#magic");
    
    var left = 0;
    var width = 0;
    if ($("#example li.current").html()) {
        left = $("#example li.current a").position().left;
        width = $("#example li.current").width();
    }

        $magicLine
            .width(width)
            .css("left", left)
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