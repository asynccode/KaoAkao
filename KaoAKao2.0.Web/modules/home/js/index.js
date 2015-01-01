define(function (require, exports, module) {
    var Global = require("global");
     
    var Index = {};
    
    Index.init = function () {
        Index.bindEvent();
    };

    Index.bindEvent = function () {
        //搜索事件
        $("#btn_search").bind("click", function () {
            if (Global.isMoblieTerminal())
            {
                $('.h-m-right,.m-s-cont').show();
                $('.h-m-right a').addClass("play");
            }
            else
            {
                $('.search-input').show();
            }
        });
    };

    module.exports = Index;
});

