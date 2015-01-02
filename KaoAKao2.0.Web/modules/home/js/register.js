define(function (require, exports, module) {
    var Global = require("global");

    var Register = {};

    //基本参数
    Register.options = {
        ajaxUrl: "/Home/UserRegister",
        userName: "",
        userPwd: "",
        userConfirmPwd: "",
        isEmail:1
    };

    //初始化
    Register.init = function () {
        Register.bindEvent();
    };

    //绑定事件
    Register.bindEvent = function () {
        $("#txt_userName").bind("blur", function () {
            var userName = $("#txt_userName").val();
            if (userName != "") {
                if ((!RegExp.isEmail(userName)) && (!RegExp.isMobile(userName)))
                {
                    Global.showIptMsg($("#txt_userName"), "用户名输入错误");
                }
                else
                {
                    $("#registerError").hide();
                }
            }
            else
            {
                Global.showIptMsg($("#txt_userName"), "用户名不能为空");
            }
        });

        $("#txt_userPwd").bind("blur", function () {
            var userPwd = $("#txt_userPwd").val();
            if (userPwd != "") {
                $(this).parent().find(".hint").hide();
            }
            else
            {
                Global.showIptMsg($("#txt_userPwd"), "密码不能为空");
            }

        });

        $("#txt_userConfirmPwd").bind("blur", function () {
            var userConfirmPwd = $("#txt_userConfirmPwd").val();
            if (userConfirmPwd != "") {
                if ( $("#txt_userPwd").val() != $("#txt_userConfirmPwd").val() ) {
                        Global.showIptMsg($("#txt_userConfirmPwd"), "确认密码错误");
                    }
                    else
                    {
                        $(this).parent().find(".hint").hide();
                    }
            }
            else
            {
                Global.showIptMsg($("#txt_userConfirmPwd"), "确认密码不能为空");
            }

        });

        $("#btn_register").bind("click", function () {
            Register.userRegister();
        });
    };

    //用户注册
    Register.userRegister = function () {
        $("#btn_register").val("注册中...");
        $("#btn_register").attr("disabled", true);

        if (Register.validate()) {
            Global.AjaxRequest(Register.options.ajaxUrl, "post",
                {
                    UserName: Register.options.userName,
                    UserPwd: Register.options.userPwd,
                    IsEmail: Register.options.isEmail,
                    Code: Register.options.code
                },
                function (data) {
                    $("#btn_register").val("注册");
                    $("#btn_register").removeAttr("disabled");

                    if (data.result == 3) {
                        Global.showIptMsg($("#txt_userName"), "用户名已存在");
                    }
                    else if (data.result == 2) {
                        Global.showIptMsg($("#txt_code"), "验证码有误");
                        $("#btn_chkCode").click();
                    }
                    else if (data.result == 1) {
                        alert("注册成功");
                        location.href = "/home/index";
                    }
                    else if (data.result == 0)
                    {
                        alert("注册失败");
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
    Register.validate = function () {
        Register.options.userName = $("#txt_userName").val();
        if (Register.options.userName) {
            if ((!RegExp.isEmail(Register.options.userName)) && (!RegExp.isMobile(Register.options.userName))) {
                Global.showIptMsg($("#txt_userName"), "用户名有误");
                return false;
            }
            else {
                if (RegExp.isMobile(Register.options.userName))
                {
                    Register.options.isEmail = 0;
                }
            }
        }
        else
        {
            Global.showIptMsg($("#txt_userName"), "用户名不能为空");
            return false;
        }

        Register.options.userPwd = $("#txt_userPwd").val();
        if (Register.options.userPwd == "") {
            Global.showIptMsg($("#txt_userPwd"), "密码不能为空");
            return false;
        }

        Register.options.userConfirmPwd = $("#txt_userConfirmPwd").val();
        if (Register.options.userConfirmPwd) {
            if (Register.options.userPwd != Register.options.userConfirmPwd) {
                Global.showIptMsg($("#txt_userConfirmPwd"), "确认密码错误");
                return false;
            }
        }
        else {
            Global.showIptMsg($("#txt_userConfirmPwd"), "确认密码不能为空");
            return false;
        }

        Register.options.code = $("#txt_code").val();
        if (Register.options.code == "") {
            Global.showIptMsg($("#txt_code"), "验证码不能为空");
            return false;
        }
        return true;
    }

    module.exports = Register;
});