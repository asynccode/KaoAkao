$(function () {
    var register = {};

    //基本参数
    register.options = {
        ajaxUrl: "/Ajax/Register",
        userName: "",
        userPwd: "",
        userConfirmPwd: "",
        isEmail:1
    };

    //初始化
    register.init = function () {
        register.bindEvent();
    };

    //绑定事件
    register.bindEvent = function () {
        $("#txt_userName").bind("blur", function () {
            var userName = $("#txt_userName").val();
            if (userName != "") {
                if ((!RegExp.isEmail(userName)) && (!RegExp.isMobile(userName)))
                {
                    $("#registerError").show();
                    $("#registerError").html("用户名输入错误！");
                }
                else
                {
                    $("#registerError").hide();
                }
            }
            else
            {
                $("#registerError").hide();
            }
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

        $("#btn_register").bind("click", function () {
            register.userRegister();
        });
    };

    //用户注册
    register.userRegister = function () {
        $("#btn_register").val("注册中...");
        $("#btn_register").attr("disabled", true);

        if (register.validate()) {
            AjaxRequest(register.options.ajaxUrl, "post",
                {
                    UserName: register.options.userName,
                    UserPwd: register.options.userPwd,
                    IsEmail:register.options.isEmail
                },
                function (data) {
                    if (data.result == "1") {
                        alert("注册成功");
                        location.href = "/home/index";
                    }
                    else {
                        alert("注册成功");
                    }
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
        register.options.userName = $("#txt_userName").val();
        if (register.options.userName) {
            if ((!RegExp.isEmail(register.options.userName)) && (!RegExp.isMobile(register.options.userName))) {
                $("#registerError").show();
                $("#registerError").html("用户名输入错误！");
                return false;
            }
            else {
                if (RegExp.isMobile(register.options.userName))
                {
                    register.options.isEmail = 0;
                }
            }
        } else {
            $("#registerError").show();
            $("#registerError").html("用户名不能为空！");
            return false;
        }

        register.options.userPwd = $("#txt_userPwd").val();
        if (register.options.userPwd == "") {
            $("#registerError").show();
            $("#registerError").html("密码不能为空！");
            return false;
        }

        register.options.userConfirmPwd = $("#txt_userConfirmPwd").val();
        if (register.options.userConfirmPwd) {
            if (register.options.userPwd != register.options.userConfirmPwd) {
                $("#registerError").show();
                $("#registerError").html("确认密码错误！");
                return false;
            }
        }
        else {
            $("#registerError").show();
            $("#registerError").html("确认密码不能为空！");
            return false;
        }

        return true;
    }

    register.init();
});