$(function () {

    var index = {};
    
    index.init= function () {
        index.bindEvent();
    };

    index.bindEvent = function () { 
        //搜索事件
        $("#btn_search").bind("click", function () {
            if(isMoblieTerminal())
            {
                $('.h-m-right').show();
                $('.m-s-cont').show();
                $('.h-m-right a').addClass("play");
            }
            else
            {
                $('.search-input').show();
            }
        });

        //
        $("#txt_userName,#txt_userPwd").bind("focus", function () {
            $(this).parent().find(".hint").show();
        });

        $("#txt_userName,#txt_userPwd").bind("blur", function () {
            $(this).parent().find(".hint").hide();
        });

    };

    index.init();
});

