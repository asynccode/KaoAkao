/*
    课程章节管理JS文件
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Verify = require('verify'), VerifyObject,
        Global = require("global"),
        doT = require("dot");
    var Params = {
        CourseID: ""
    };
    var CourseLesson = {
        //新建页JS
        initCreate: function (id) {
            Params.CourseID = id;
            this.bindCreateEvent();
        },
        //绑定事件
        bindCreateEvent: function () {
            var _self = this;
            VerifyObject = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyReg: "data-type",
                regText: "data-text"
            });
            $("#btnSaveLesson").click(function () {
                if (!VerifyObject.isPass())
                    return;
                _self.savaLesson();
            });
        },
        //保存分类
        savaLesson : function () {
            var _self = this;
            var Lesson = {
                LessonID: _self.LessonID,
                LessonName: $("#lessonName").val().trim(),
                CourseID: Params.CourseID,
                PID: $("#pLesson").val(),
                RadioURL: $("#videoURL").val(),
                RadioSize: $("#videoSize").val(),
                Sort: $("#sort").val(),
                Keywords: $("#lessonKeyWords").val(),
                Description: $("#description").val()
            };

            Global.post("/Manage/Course/SaveCourseLesson", { lesson: JSON.stringify(Lesson) }, function (data) {
                if (data.ID.length > 0) {
                    location.href = "/Manage/Course/Lessons/" + Params.CourseID;
                }
            });

        },

        //详情页JS
        initDetail: function (id, courseid ,callBack) {
            this.LessonID = id;
            Params.CourseID = courseid;
            this.bindCreateEvent();
            !!callBack && callBack();
            //this.bindLesson(id, callBack);
        },
        //绑定页面信息
        //bindLesson: function (id, callBack) {
        //    Global.post("/Manage/Courses/GetCourseCategoryByID", { cid: id }, function (data) {
        //        var model = data.Item;
        //        $("#categoryName").val(model.CategoryName);
        //        $("#pcategory").val(model.PID);
        //        $("#categoryImage").attr("src", model.CategoryImg);
        //        $("#categoryKeyWords").val(model.KeyWords);
        //        $("#description").val(model.Description);
                
        //    });
        //}
    };


    module.exports = CourseLesson;
})