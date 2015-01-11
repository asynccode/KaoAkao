define(function (require, exports, module) {
    var Global = require("global");
    var DoT = require("dot");

    var Index = {};
    
    Index.init = function () {
        Index.bindEvent();

        Index.getGoodCourses();
    };

    Index.bindEvent = function () {

    };

    Index.getGoodCourses = function (cID) {
        Global.AjaxRequest("/Home/GetGoodCourses", "post",
        null,
            function (data) {
                if (data.result = 1) {

                    DoT.exec("/modules/home/template/courseOfIndex.html", function (templateFun) {
                        var innerText = templateFun(data.courses);
                        innerText = $(innerText);
                        $(".content7 .clearfix").append(innerText).fadeIn();
                    });
                }
            });
    };

    module.exports = Index;
});

