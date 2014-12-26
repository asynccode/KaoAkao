
$(function () {

    var login = {};

    //基本参数
    login.options = {
        ajaxUrl: "/Ajax/Login",
        userName: "",
        userPwd: "",
        code:""
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
        $("#btn_userLogin").attr("disabled", true);

        if (login.validate()) {
            AjaxRequest(login.options.ajaxUrl, "post",
                {
                    Name: login.options.userName,
                    Pwd: login.options.userPwd
                },
                function (data) {
                    if (data.result == 0) {
                        $("#btn_userLogin").val("登录");
                        $("#btn_userLogin").removeAttr("disabled");
                        $("#loginError").html("输入的用户名或密码有误！").show();
                    }
                    else {
                        location.href = "/home/index"
                    }
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
        
        if (login.options.userName != "") {
            if ((!RegExp.isEmail(login.options.userName)) && (!RegExp.isMobile(login.options.userName))) {
                $("#loginError").html("输入的用户名有误！").show();
                return false;
            }
        }
        else {
            $("#loginError").html("输入的用户名不能为空！").show();
            return false;
        }

        login.options.userPwd = $("#txt_userPwd").val();
        if (login.options.userPwd == "") {
            $("#loginError").html("输入密码不能为空！").show();
            return false;
        }

        return true;
    }

    login.init();
});