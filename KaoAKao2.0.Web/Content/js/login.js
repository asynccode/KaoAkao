
$(function () {

    var login = {};

    //基本参数
    login.options = {
        ajaxUrl:"",
        userName: "",
        userPwd:""
    };

    //初始化
    login.init = function () {
        login.bindEvent();
    };

    //绑定事件
    login.bindEvent = function () {
        $("#txt_userName").bind("focus", function () {
            var userName=$("#txt_userName").val();
            if(userName!="")
                {
                    if ( (!RegExp.isEmail(userName)) && (!RegExp.isMobile(userName)) )
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
            if (userPwd != "")
            {
                $(this).parent().find(".hint").hide();
            }
            else
            {
                $(this).parent().find(".hint").show();
            }

        });

        $("#btn_userLogin").bind("click", function () {
            login.userLogin();
        });
    };

    //用户登录
    login.userLogin = function () {
        $("#btn_userLogin").val("登录中...");
        $("#btn_userLogin").attr("disabled",true);
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
            $("#btn_userLogin").val("登录");
            $("#btn_userLogin").removeAttr("disabled");
        }
    };

    //数据验证
    login.validate = function () {
        login.options.userName = $("#txt_userName").val();
        login.options.userPwd = $("#txt_userPwd").val();

        if ((!RegExp.isEmail(login.options.userName)) && (!RegExp.isMobile(login.options.userName)))
            return false;
        if (login.options.userPwd)
            return false;

        return true;
    }

    login.init();
});