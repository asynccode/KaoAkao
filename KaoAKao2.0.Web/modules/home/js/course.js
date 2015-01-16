define(function (require, exports, module) {
    var Global = require("global");
    var DoT = require("dot");
    var Paginate = require("paginatewithhome");


    var Course = {};

    //基本参数
    Course.options = {
        ajaxUrl: "/Home/GetCourses",
        pageIndex: 1,
        cID: '',
        cOrderType: 0,
        orderType:1
    };

    //初始化
    Course.init = function () {
        Course.bindEvent();

        Course.getCourseCategorys();

        Course.getCourses('');
        
        Course.getTeachers();
    };

    //绑定事件
    Course.bindEvent = function () {
        $("#ul_cOrder li").bind("click", function () {
            $(this).parent().slideUp();

            var cOrderType = $(this).attr("cOrderType");
            Course.options.cOrderType = parseInt(cOrderType);
            Course.getCourses(Course.options.cID);
        });

        $("#ul_order li").bind("click", function () {
            $(this).parent().slideUp();

            var orderType = $(this).attr("orderType");
            Course.options.orderType = parseInt(orderType);
            Course.options.pageIndex = 1;
            Course.getCourses(Course.options.cID);
        });
    };

    Course.getCourseCategorys = function () {
        Course.options.ajaxUrl = "/home/GetCourseCategorys";
        Global.AjaxRequest(Course.options.ajaxUrl, "post",
            null,
            function (data) {
                if (data.result == 1) {
                    var len = data.categorys.length;
                    for (var i = 0; i < len; i++) {
                        var item = data.categorys[i];
                        var html = '<li><a href="javascript:void(0);" class="csy-0' + (i + 1) + '" BindCID=' + item.CategoryID + '><i></i>' + item.CategoryName + '</a></li>';
                        $(".content11 .clearfix").append(html);

                        html = '<li><a href="javascript:void(0);" class="csy2-0' + (i + 1) + '" BindCID=' + item.CategoryID + '>' + item.CategoryName + '</a></li>';
                        $(".content15 .clearfix").append(html);

                        if (i == 0)
                            html = '<li><a href="javascript:void(0);" class="active" BindCID=' + item.CategoryID + '>' + item.CategoryName + '</a></li>';
                        else
                            html = '<li><a href="javascript:void(0);" BindCID=' + item.CategoryID + '>' + item.CategoryName + '</a></li>';
                        $(".content16 .clearfix").append(html);
                    }

                    $(".content11 .clearfix li a,.content15 .clearfix li a,.content16 .clearfix li a").bind("click", function () {
                        Course.options.pageIndex = 1;
                        $(this).parent().parent().find("li a").removeClass("active");
                        $(this).addClass("active");

                        var cID = $(this).attr("BindCID");
                        Course.getCourses(cID);
                    });

                }
            });
    };

    Course.getCourses = function (cID) {
        Course.options.ajaxUrl = "/Home/GetCourses";
        if (cID)
            Course.options.cID = cID;
        else
            Course.options.cID = '';

        Global.AjaxRequest(Course.options.ajaxUrl, "post",
            {
                PageIndex: Course.options.pageIndex,
                CID: Course.options.cID,
                OrderType: Course.options.orderType
            },
            function (data) {
                if (data.result == 1) {
                    $(".e-nr .clearfix").html('');

                    DoT.exec("/modules/home/template/course.html", function (templateFun) {
                        var innerText = templateFun(data.courses);
                        innerText = $(innerText);
                        $(".e-nr .clearfix").append(innerText).fadeIn();
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
                            display: 2,
                            border: true,
                            border_color: '#fff',
                            text_color: '#333',
                            background_color: '#fff',
                            rotate: true,
                            images: true,
                            mouse: 'slide',
                            onChange: function (page) {
                                Course.options.pageIndex = page;
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
        html+='<dt><a href="#"><img src="/modules/home/images/index_16.jpg" alt="David"/></a></dt>';
        html += '<dd><a href="#">' + item.KeyWords + '</a></dd>';
        html += '<dd><a href="#">' + item.KeyWords + '</a></dd>';
        html += '</dl>';

        return html;
    };

    module.exports= Course;
});