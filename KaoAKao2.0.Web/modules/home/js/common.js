define(function (require, exports, module) {
    var Global = require("global");

    var Common = {};

    Common.init = function () {
        Common.bindEvent();

        //验证用户是否登录
        Common.validateLogin();
    };

    Common.bindEvent = function () {
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

    //验证用户是否登录
    Common.validateLogin = function () {
        Global.AjaxRequest("/Home/Validate", "get", null,
            function (data) {
                if (data.result == 1) {
                    $("#ul_nav a.button-min").hide();

                    $("#ul_nav .user").show();
                    $("#ul_nav .user .effigy").append("<i><img src='/modules/home/Images/index_16.jpg' /></i>" + data.userName);

                    var mst = null;
                    $("#ul_nav .user").hover(function () {
                        mst = setTimeout(function () {//延时触发
                            mst = null;
                            $("#ul_nav .user .user-center").slideDown('fast');
                        });
                    },
                    function () {
                        if (mst != null) clearTimeout(mst);
                        $("#ul_nav .user .user-center").slideUp("fast");
                    }

                    );
                }
            }
        );
    }
    module.exports = Common;
});

