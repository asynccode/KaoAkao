
define(function (require, exports, module) {
    var Global = require("global");

    var Login = {};

    //基本参数
    Login.options = {
        ajaxUrl: "/Home/UserLogin",
        userName: "",
        userPwd: "",
        code: "",
        autoLogin:0
    };

    //初始化
    Login.init = function () {
        var passportInfo = Global.getCookie("passportInfo");
        if (passportInfo!=null)
        {
            $("#txt_userName").val( passportInfo.split('|')[0] );
            $("#txt_userPwd").val(passportInfo.split('|')[1] );
            Login.userLogin();
        }
        else
            Login.bindEvent();
    };

    //绑定事件
    Login.bindEvent = function () {
        $("#txt_userName").bind("focus", function () {
            var userName=$("#txt_userName").val();
            if(userName!="")
            {
                if ( (!RegExp.isEmail(userName)) && (!RegExp.isMobile(userName)) )
                    Global.showIptMsg($("#txt_userName"), "用户名有误");
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
                    Global.showIptMsg($("#txt_userName"), "用户名有误");
                else
                    $(this).parent().find(".hint").hide();
            }
            else
                Global.showIptMsg($("#txt_userName"), "用户名不能为空");
        });

        $("#txt_userPwd").bind("blur", function () {
            var userPwd = $("#txt_userPwd").val();
            if (userPwd != "")
                $(this).parent().find(".hint").hide();
            else
                Global.showIptMsg($("#txt_userPwd"), "密码不能为空");

        });

        $("#btn_userLogin").bind("click", function () {
            Login.userLogin();
        });

        $("#btn_autoLogin").bind("click", function () {
            if ($(this).hasClass("checked")) {
                $(this).removeClass("checked");
            }
            else {
                $(this).addClass("checked");
            }
        });
    };

    //用户登录
    Login.userLogin = function () {
        $("#btn_userLogin").val("登录中...");
        $("#btn_userLogin").attr("disabled", true);
        if ($("#btn_autoLogin").hasClass("checked"))
            Login.options.autoLogin = 1;

        if (Login.validate()) {
            Global.AjaxRequest(Login.options.ajaxUrl, "post",
                {
                    Name: Login.options.userName,
                    Pwd: Login.options.userPwd
                },
                function (data) {
                    $("#btn_userLogin").val("登录");
                    $("#btn_userLogin").removeAttr("disabled");

                    if (data.result == 0) {
                        $("#loginError").html("用户名或密码有误!").show();
                    }
                    else
                    {
                        if (Login.options.autoLogin == 1)
                        {
                            Global.setCookie("passportInfo", Login.options.userName + "|" + Login.options.userPwd);
                        }

                        if (data.isUrlReferrer==0)
                            location.href = "/home/index";
                        else
                            history.go(-1);
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
    Login.validate = function () {
        Login.options.userName = $("#txt_userName").val();
        
        if (Login.options.userName != "") {
            if ((!RegExp.isEmail(Login.options.userName)) && (!RegExp.isMobile(Login.options.userName))) {
                Global.showIptMsg($("#txt_userName"), "用户名有误");
                return false;
            }
        }
        else
        {
            Global.showIptMsg($("#txt_userName"), "用户名不能为空");
            return false;
        }

        Login.options.userPwd = $("#txt_userPwd").val();
        if (Login.options.userPwd == "") {
            Global.showIptMsg($("#txt_userPwd"), "密码不能为空");
            return false;
        }

        return true;
    }

    module.exports = Login;
});