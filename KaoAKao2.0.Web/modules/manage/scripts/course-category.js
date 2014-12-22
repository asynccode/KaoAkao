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
            this.getCategoryList(1);
        },
        //绑定事件
        bindEvent: function () {
            var _self = this;
            $(".search-ico").click(function () {
                Params.keywords = $("#keywords").val();
                _self.getCategoryList(1);
            });
            $("#keywords").keypress(function (event) {
                if (event.keyCode == 13) {
                    Params.keywords = $("#keywords").val();
                    _self.getCategoryList(1);
                }
            });
            $("#category").change(function () {
                Params.pid = $(this).val();
                _self.getCategoryList(1);
            })
        },
        //获取列表
        getCategoryList: function () {
            var _self = this;
            $(".tr-header").nextAll().remove();
            Global.post("/Manage/Course/GetCourseCategorys", Params, function (data) {
                doT.exec("/modules/manage/template/course-category-list.html", function (templateFun) {
                    var innerText = templateFun(data.Items);
                    innerText = $(innerText);
                    $(".tr-header").after(innerText);
                    //绑定事件
                    innerText.find(".delete").click(function () {
                        var cid = $(this).data("id");
                        if (confirm("确认删除此分类和其子类吗？")) {
                            Global.post("/Manage/Course/DeleteCourseCategoy", {
                                categoyid: cid
                            }, function (data) {
                                if (data.Status) {
                                    _self.getCategoryList(1);
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
                        _self.getCategoryList();
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
            $("#btnSavecategory").click(function () {
                if (!VerifyObject.isPass())
                    return;
                _self.savaCategory();
            })
        },
        //保存分类
        savaCategory : function () {
            var _self = this;
            var Category = {
                CategoryID: _self.CategoryID,
                CategoryName: $("#categoryName").val().trim(),
                PID: $("#pcategory").val(),
                KeyWords: $("#categoryKeyWords").val(),
                Description: $("#description").val()  
            };
            Global.post("/Manage/Course/SaveCourseCategoy", { categoy: JSON.stringify(Category) }, function (data) {
                if (data.ID.length > 0) {
                    location.href = "/Manage/Course/Categorys"
                }
            });
        },

        //详情页JS
        initDetail: function (id, callBack) {
            this.CategoryID = id;
            this.bindCreateEvent();
            this.bindCategory(id, callBack);
        },
        //绑定页面信息
        bindCategory: function (id, callBack) {
            Global.post("/Manage/Course/GetCourseCategoryByID", { cid: id }, function (data) {
                var model = data.Item;
                $("#categoryName").val(model.CategoryName);
                $("#pcategory").val(model.PID);
                $("#categoryKeyWords").val(model.KeyWords);
                $("#description").val(model.Description);
                !!callBack && callBack();
            });
        }
    };


    module.exports = CourseCategory;
})