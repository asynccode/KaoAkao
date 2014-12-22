$(function () {
    var register = {};

    //基本参数
    register.options = {
        ajaxUrl: "",
        userName: "",
        userPwd: ""
    };

    //初始化
    register.init = function () {
        register.bindEvent();
    };

    //绑定事件
    register.bindEvent = function () {
        $("#txt_userEmail").bind("focus", function () {
            var userName = $("#txt_userEmail").val();
            if (userName != "") {
                if ((!RegExp.isEmail(userName)) && (!RegExp.isMobile(userName)))
                    $(this).parent().find(".hint").show();
                else
                    $(this).parent().find(".hint").hide();
            }
        });

        $("#txt_userPwd").bind("focus", function () {
            var userPwd = $("#txt_userPwd").val();
            if (userPwd != "") {
                $(this).parent().find(".hint").hide();
            }
        });

        $("#txt_userName").bind("blur", function () {
            var userName = $("#txt_userName").val();
            if (userName != "") {
                if ((!RegExp.isEmail(userName)) && (!RegExp.isMobile(userName)))
                    $(this).parent().find(".hint").show();
                else
                    $(this).parent().find(".hint").hide();
            }
            else
                $(this).parent().find(".hint").show();
        });

        $("#txt_userPwd").bind("blur", function () {
            var userPwd = $("#txt_userPwd").val();
            if (userPwd != "") {
                $(this).parent().find(".hint").hide();
            }
            else {
                $(this).parent().find(".hint").show();
            }

        });

        $("#btn_userLogin").bind("click", function () {
            login.userLogin();
        });
    };

    //用户注册
    register.userRegister = function () {
        $("#btn_register").val("注册中...");
        $("#btn_register").attr("disabled", true);
        if (login.validate()) {
            alert(login.options.userName);
            AjaxRequest(login.options.ajaxUrl, "post",
                {
                    userName: login.options.userName,
                    userPwd: login.options.userPwd
                },
                function (data) {

                }
                );
        }
        else {
            $("#btn_register").val("注册");
            $("#btn_register").removeAttr("disabled");
        }
    };

    //数据验证
    register.validate = function () {
        login.options.userName = $("#txt_userName").val();
        login.options.userPwd = $("#txt_userPwd").val();

        if ((!RegExp.isEmail(login.options.userName)) && (!RegExp.isMobile(login.options.userName)))
            return false;
        if (login.options.userPwd)
            return false;

        return true;
    }

    register.init();
});