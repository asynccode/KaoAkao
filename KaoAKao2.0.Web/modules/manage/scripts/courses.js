/*
    课程分类管理JS文件
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Verify = require('verify'), VerifyObject,
        Global = require("global"),
        doT = require("dot");
    require("paginate");
    var Params = {
        pid: -1,
        index: 1,
        keywords: "",
    }
    var CourseCategory = {

        //列表页JS
        init : function () {
            this.bindEvent();
            this.getCourseList(1);
        },
        //绑定事件
        bindEvent: function () {
            var _self = this;
            $(".search-ico").click(function () {
                Params.keywords = $("#keywords").val();
                _self.getCourseList(1);
            });
            $("#keywords").keypress(function (event) {
                if (event.keyCode == 13) {
                    Params.keywords = $("#keywords").val();
                    _self.getCourseList(1);
                }
            });
            $("#category").change(function () {
                Params.pid = $(this).val();
                _self.getCourseList(1);
            })
        },
        //获取列表
        getCourseList: function () {
            var _self = this;
            $(".tr-header").nextAll().remove();
            Global.post("/Manage/Courses/GetCourses", Params, function (data) {
                doT.exec("/modules/manage/template/course-list.html", function (templateFun) {
                    var innerText = templateFun(data.Items);
                    innerText = $(innerText);
                    $(".tr-header").after(innerText);
                    //绑定事件
                    innerText.find(".delete").click(function () {
                        var cid = $(this).data("id");
                        if (confirm("确认删除课程吗？")) {
                            Global.post("/Manage/Courses/DeleteCourse", {
                                courseid: cid
                            }, function (data) {
                                if (data.Status) {
                                    _self.getCourseList(1);
                                }
                            })
                        }
                    });
                });
                $("#pager").paginate({
                    total_count: data.Total,
                    count: data.Pages,
                    start: Params.index,
                    display: 10,
                    border: true,
                    border_color: '#fff',
                    text_color: '#333',
                    background_color: '#fff',
                    border_hover_color: '#ccc',
                    text_hover_color: '#333',
                    background_hover_color: '#ee',
                    rotate: true,
                    images: false,
                    mouse: 'slide',
                    onChange: function (page) {
                        Params.index = page;
                        _self.getCourseList();
                    }
                });
            });
        },

        //新建页JS
        initCreate: function () {
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
            $("#category").change(function () {
                _self.getChildCategry($(this).val());
            });
            $("#category").change();
            $("#btnSaveCourse").click(function () {
                if (!VerifyObject.isPass())
                    return;
                _self.savaCourse();
            })
        },
        //保存分类
        savaCourse: function () {
            var _self = this;
            var Course = {
                CourseID: _self.CourseID,
                CourseName: $("#courseName").val().trim(),
                CategoryID: $("#pcategory").val(),
                CourseImg: $("#imageURL").val(),
                KeyWords: $("#courseKeyWords").val(),
                Description: $("#description").val(),
                TeacherID: $("#teacher").val()
            };
            if (!Course.CourseID) {
                Global.post("/Manage/Courses/AddCourse", { course: JSON.stringify(Course) }, function (data) {
                    if (data.ID.length > 0) {
                        location.href = "/Manage/Courses/Courses"
                    }
                });
            } else {
                Global.post("/Manage/Courses/EditCourse", { course: JSON.stringify(Course) }, function (data) {
                    if (data.Status) {
                        location.href = "/Manage/Courses/Courses"
                    }
                });
            }
        },
        //获取下级分类
        getChildCategry: function (pid, callback) {
            $("#pcategory").empty();
            Global.post("/Manage/Courses/GetCourseCategorysByPID", { pid: pid }, function (data) {
                for (var i = 0, j = data.Items.length; i < j; i++) {
                    $("#pcategory").append("<option value='" + data.Items[i].CategoryID + "'>" + data.Items[i].CategoryName + "</option>");
                }
                !!callback && callback();
            });
        },
        //详情页JS
        initDetail: function (id, callBack) {
            this.CourseID = id;
            this.bindCreateEvent();
            this.bindCourse(id, callBack);
        },
        //绑定页面信息
        bindCourse: function (id, callBack) {
            var _self = this;
            Global.post("/Manage/Courses/GetCourseByID", { courseid: id }, function (data) {
                var model = data.Item;

                $("#courseName").val(model.CourseName);
                $("#category").val(model.PID);
                _self.getChildCategry(model.PID, function () {
                    $("#pcategory").val(model.CategoryID);
                });
                $("#courseImage").attr("src", model.CourseImg);
                $("#imageURL").val(model.CourseImg);
                $("#courseKeyWords").val(model.KeyWords);
                $("#description").val(model.Description);
                $("#teacher").val(model.TeacherID);
                !!callBack && callBack();
            });
        }
    };


    module.exports = CourseCategory;
})