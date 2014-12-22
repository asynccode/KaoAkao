﻿
/*

    --数据验证控件--

    --引用
    Verify = require("verify")

    --实例化
    VerifyObject = Verify.createVerify({
        element: ".verify",             //触发元素
        emptyAttr: "data-empty",        //为空判断
        verifyReg: "data-reg",          //正则匹配
        regText: "data-text"            //匹配失败提示
    });
    
    VerifyObject.isPass()              //判断是否通过

*/
define(function (require, exports, module) {
    var $ = require("jquery");

    var Verify = function (options) {
        var _this = this;
        _this.setting = $.extend([], _this.default, options);
        _this.setting.type = [];
        _this.setting.type["mobile"] = /^1[3|5|6|8]\d{9}$/;
        _this.setting.type["email"] = /^\w+(.\w+)+@\w+.\w+$/;
        _this.init();
    }
    Verify.Reg = [];
    //默认参数
    Verify.prototype.default = {
        element: ".verify",
        emptyAttr: "data-empty",
        verifyReg: "data-type",
        regText: "data-text"
    };
    //初始化插件
    Verify.prototype.init = function () {
        var _self = this;
        
        $(_self.setting.element).on("blur", function () {
            var _this = $(this);

            //为空验证
            if (!!_this.attr(_self.setting.emptyAttr) && !_this.val()) {
                if (_this.next().attr("class") != "verify-fail-err") {
                    _this.css("border-color", "red");
                    _this.after($("<span class=\"verify-fail-err\">" + _this.attr(_self.setting.emptyAttr) + "</span>").css("color", "red"));
                } else {
                    _this.next().html(_this.attr(_self.setting.emptyAttr));
                }
            } //数据类型验证
            else if (!!_this.attr(_self.setting.verifyReg) && !!_this.val() && !_this.val().match(_self.setting.type[_this.attr(_self.setting.verifyReg)])) {
                _this.val("");
                if (_this.next().attr("class") != "verify-fail-err") {
                    _this.css("border-color", "red");
                    _this.after($("<span class=\"verify-fail-err\">" + _this.attr(_self.setting.regText) + "</span>").css("color", "red"));
                } else {
                    _this.next().html(_this.attr(_self.setting.regText));
                }
            }
            else {
                _this.css("border-color", "#ccc");
                _this.next().remove();
            }
        });
    }
    //提交验证
    Verify.prototype.isPass = function () {
        var _self = this;
        $(_self.setting.element).blur();
        return $(".verify-fail-err").length == 0;
    }
    exports.createVerify = function (options) {
        return new Verify(options);
    }
});