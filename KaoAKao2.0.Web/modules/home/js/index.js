define(function (require, exports, module) {
    var Global = require("global");
     
    var Index = {};
    
    Index.init = function () {
        Index.bindEvent();
    };

    Index.bindEvent = function () {
        //搜索事件
        $("#btn_search").bind("click", function () {
                $('.search-input').show();
        });

        $("#btn_mobileGenreSearch").bind("click", function () {
            $(".content0").show();
            $("#btn_mobileSearch").next().hide();
            $("#btn_mobileSearch").removeClass("play");

            $(this).addClass("play");
            $(this).next().show();
        });

        $("#btn_mobileSearch").bind("click", function () {
            $(".content0").show();
            $("#btn_mobileGenreSearch").next().hide();
            $("#btn_mobileGenreSearch").removeClass("play");

            $(this).addClass("play");
            $(this).next().show();
        });

    };

    module.exports = Index;
});

