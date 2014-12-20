﻿/*
*布局页JS
*/
define(function (require, exports, module) {
    var $ = require("jquery"), Global = require("global");
    var Height = document.documentElement.clientHeight - 84, Width = document.documentElement.clientWidth;
    var LayoutObject = {};
    //初始化数据
    LayoutObject.init = function () {
        LayoutObject.bindStyle();
        LayoutObject.bindEvent();
    }
    //绑定元素定位和样式
    LayoutObject.bindStyle = function () {
        var _height = Height, _width = Width - 190;
        $("nav").css("height", _height);
        $(".controlbar").css("height", _height);
        $(".main-content").css({ "height": _height, "width": _width });
        $(".ico-left-pull").css("top", _height / 2 - 32);

    }
    //绑定事件
    LayoutObject.bindEvent = function () {
        var _height = Height - 65 - $(".controller").length * 29;

        //展开三级菜单
        $(".controller").bind("click", function () {
            $(".controller").removeClass("select").css("height", "28px");
            $(this).addClass("select").css("height", _height + 29);
            $(this).find("ul").css("height", _height);
        });
        //同比例放大按钮
        $(".ico-left-pull").hover(function () {
            var _this = $(this);
            _this.css("top", Height / 2 - 40);
        }, function () {
            var _this = $(this);
            _this.css("top", Height / 2 - 32);
        });
        //隐藏、展开左侧菜单
        $(".ico-left-pull").click(function () {
            var _this = $(this);
            if (_this.attr("data-status") == "open") {
                _this.attr("data-status", "close");
                $("nav").animate({ left: "-186px" }, "fast");
                $(".controlbar").animate({ left: "0px" }, "fast");
                _this.animate({ left: "2px" }, "fast");
                _this.removeClass("ico-left-pull").addClass("ico-left-open");
                $(".main-content").css("width", Width - 4);
            } else {
                _this.attr("data-status", "open");
                $("nav").animate({ left: "0px" }, "fast");
                $(".controlbar").animate({ left: "186px" }, "fast");
                _this.animate({ left: "188px" }, "fast");
                _this.removeClass("ico-left-open").addClass("ico-left-pull");
                $(".main-content").css("width", Width - 190);
            }
        });
       
        $(".controller.select").click();
    }
    module.exports = LayoutObject;
})