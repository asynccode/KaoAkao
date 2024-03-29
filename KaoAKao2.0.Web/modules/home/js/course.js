﻿define(function (require, exports, module) {
    var Global = require("global");
    var DoT = require("dot");
    var Paginate = require("paginatewithhome");


    var Course = {};

    //基本参数
    Course.options = {
        ajaxUrl: "/Home/GetCourses",
        pageIndex: 1,
        pID:'',
        cID: '',
        cOrderType: 0,
        orderType:1
    };

    //初始化
    Course.init = function () {
        Course.bindEvent();

        Course.getCourses('');
        
        Course.getTeachers();
    };

    //绑定事件
    Course.bindEvent = function () {
        $(window).unbind().bind("scroll", function () {
            var width = document.body.offsetWidth;
            if (width < 1200) {
                var bottom = $(document).height() - document.documentElement.scrollTop - document.body.scrollTop - $(window).height();
                if (bottom <= 50) {
                    $("#pager").hide();

                    setTimeout(function () {
                        Course.options.pageIndex++;
                        Course.getCourses();
                    }, 1000);
                }
            }

        });

        window.onresize = function () {
            var width = document.body.offsetWidth;
            if (width < 1200) {
                $("#pager").hide();
            }
            else {
                $("#pager").show();
            }
        }

        $("#a_courseOrder,#a_courseCid").bind("click", function (event) {
            event.stopPropagation();
            $(this).next().slideToggle();
        });

        $(document).on('click', function (e) {
            $("#ul_order").slideUp();
            $("#ul_cOrder").slideUp();
        });

        $("#ul_order li").bind("click", function () {
            $(this).parent().slideUp();
            $("#a_courseOrder").html( $(this).find("a").html() );

            var orderType = $(this).attr("orderType");
            Course.options.orderType = parseInt(orderType);
            Course.options.pageIndex = 1;
            Course.getCourses();
        });

        $(".content11 .clearfix li a,.content15 .clearfix li a,.content16 .clearfix li a").unbind().bind("click", function () {
            Course.options.pageIndex = 1;
            $(this).parent().parent().find("li a").removeClass("active");
            $(this).addClass("active");

            var PID = $(this).attr("BindCID");
            Course.options.pID = PID;
            if (PID == -1) {
                Course.options.pID = '';
                $("#a_courseCid").parent().hide();
            }
            else {
                $("#a_courseCid").parent().show();
            }
            Course.getCourseCategorys();

            Course.options.cID = '';
            Course.getCourses();
            $("#ul_cOrder").html('');
        });

    };

    Course.getCourseCategorys = function () {
        Course.options.ajaxUrl = "/home/GetCourseCategorys";
        Global.AjaxRequest(Course.options.ajaxUrl, "post",
            {PID:Course.options.pID},
            function (data) {
                if (data.result == 1) {
                    var len = data.categorys.length;
                    if (len < 1)
                    {
                        $("#a_courseCid").parent().hide();
                        $("#a_courseOrder").parent().hide();
                    }

                    for (var i = 0; i < len; i++) {
                        var item = data.categorys[i];
                            var html = '<li BindCID=' + item.CategoryID + '><a href="javascript:void(0);" >' + item.CategoryName + '</a></li>';
                            $("#ul_cOrder").append(html);
                    }


                    $("#ul_cOrder li").unbind().bind("click", function () {
                        $(this).parent().slideUp();
                        $("#a_courseCid").html($(this).find("a").html());

                        var cID = $(this).attr("BindCID");
                        Course.options.cID = parseInt(cID);
                        Course.options.pageIndex = 1;
                        Course.getCourses();
                    });

                }

            });
    };

    Course.getCourses = function () {
        Course.options.ajaxUrl = "/Home/GetCourses";

        Global.AjaxRequest(Course.options.ajaxUrl, "post",
            {
                PageIndex: Course.options.pageIndex,
                CID: Course.options.cID,
                PID:Course.options.pID,
                OrderType: Course.options.orderType
            },
            function (data) {
                if (data.result == 1) {
                    var width = document.body.offsetWidth;
                    if (width >=1200)
                        $(".e-nr .clearfix").html('');

                    DoT.exec("/modules/home/template/course.html", function (templateFun) {
                        var innerText = templateFun(data.courses);
                        innerText = $(innerText);
                        $(".e-nr #ul_courseList").append(innerText).fadeIn();
                    });

                    var pages=parseInt(data.pages);
                    if (pages>0 && Course.options.pageIndex > pages)
                        Course.options.pageIndex = pages;

                    if (data.courses.length > 0) {
                        if ($("#noDataDiv").html())
                            $("#noDataDiv").remove();

                        $("#pager").paginate({
                            total_count: data.total,
                            count: data.pages,
                            start: Course.options.pageIndex,
                            display: 10,
                            rotate: true,
                            mouse: 'slide',
                            onChange: function (page) {
                                Course.options.pageIndex = parseInt(page);
                                Course.getCourses();
                            }
                        });

                        $("#btn_goPageIndex").unbind().bind("click", function () {
                            var goPageIndex = $("#txt_goPageIndex").val();
                            if (RegExp.isNum(goPageIndex)) {
                                Course.options.pageIndex = parseInt(goPageIndex);
                                Course.getCourses();
                            }
                            else {
                                alert("页码有误");
                                $("#txt_goPageIndex").val('');
                            }

                        });

                        var width = document.body.offsetWidth;
                        if (width < 1200)
                            $("#pager").hide();
                    }
                    else {
                        if ($("#noDataDiv").html())
                            $("#noDataDiv").remove();

                        var noDataHtml = '<div id="noDataDiv" style="padding:20px 0px;"><h3 style="text-align:center;">暂无数据！</h3></div>';
                        $(".e-nr").after().append(noDataHtml).fadeIn();
                    }

                }
            });
    };

    Course.getTeachers = function () {
        Course.options.ajaxUrl = "/home/GetTeachers";
        Global.AjaxRequest(Course.options.ajaxUrl, "post",
            {
                PageIndex: Course.options.pageIndex
            },
            function (data) {
                if (data.result == 1) {
                    var len=data.teachers.length;
                    for (var i = 0; i < len; i++)
                    {
                        var item = data.teachers[i];
                        $(".content13 #teacherList").append(Course.createTeacher(item));
                    }

                    if(len>8)
                    {
                        var html='';
                        html+='<div class="clearfix advisor-more">';
                        html+='<div><a href="#">查看更多>></a></div>';
                        html += '</div>';
                        $(".content13 #teacherList").append(html);
                    }

                }
            });
    };

    Course.createTeacher= function (item) { 
        var html='';
        html+='<dl class="clearfix">';
        html += '<dt><a href="#"><img src="' + item.PhotoPath + '" alt="' + item.UserName + '"/></a></dt>';
        html += '<dd><a href="#">' + item.KeyWords + '</a></dd>';
        html += '<dd><a href="#">' + item.UserName + '</a></dd>';
        html += '</dl>';

        return html;
    };

    module.exports= Course;
});