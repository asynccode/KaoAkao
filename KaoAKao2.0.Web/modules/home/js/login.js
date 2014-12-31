
define(function (require, exports, module) {
    

    var Login = {};

    //基本参数
    Login.options = {
        ajaxUrl: "/Home/UserLogin",
        userName: "",
        userPwd: "",
        code:""
    };

    //初始化
    Login.init = function () {
        Login.bindEvent();
    };

    //绑定事件
    Login.bindEvent = function () {
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
            Login.userLogin();
        });
    };

    //用户登录
    Login.userLogin = function () {
        $("#btn_userLogin").val("登录中...");
        $("#btn_userLogin").attr("disabled", true);

        if (Login.validate()) {
            AjaxRequest(login.options.ajaxUrl, "post",
                {
                    Name: Login.options.userName,
                    Pwd: Login.options.userPwd
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
    Login.validate = function () {
        Login.options.userName = $("#txt_userName").val();
        
        if (Login.options.userName != "") {
            if ((!RegExp.isEmail(Login.options.userName)) && (!RegExp.isMobile(Login.options.userName))) {
                $("#loginError").html("输入的用户名有误！").show();
                return false;
            }
        }
        else {
            $("#loginError").html("输入的用户名不能为空！").show();
            return false;
        }

        Login.options.userPwd = $("#txt_userPwd").val();
        if (Login.options.userPwd == "") {
            $("#loginError").html("输入密码不能为空！").show();
            return false;
        }

        return true;
    }

    module.exports = Login;
});