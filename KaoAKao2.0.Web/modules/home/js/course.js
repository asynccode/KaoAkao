define(function (require, exports, module) {
    var Global = require("global");
    var DoT = require("dot");
    var Paginate = require("paginatewithhome");
    var Course = {};

    //基本参数
    Course.options = {
        ajaxUrl: "/Home/GetCourses",
        pageIndex:1
    };

    //初始化
    Course.init = function () {
        Course.bindEvent();

        Course.getCourses();
        
        Course.getTeachers();
    };

    //绑定事件
    Course.bindEvent = function () {
       

        // 菜单导航
        $(".content11 .clearfix li").bind("click", function () {
            $(".content11 .clearfix li a").removeClass("active");
            $(this).find("a").addClass("active");
        });

        $(".content16 .clearfix li[class!='whole']").bind("click", function () {
            $(".content16 .clearfix li[class!='whole'] a").removeClass("active");
            $(this).find("a").addClass("active");
        });


    };

    Course.getCourses = function () {
        Course.options.ajaxUrl = "/Home/GetCourses";
        Global.AjaxRequest(Course.options.ajaxUrl, "post",
            {
                PageIndex: Course.options.pageIndex
            },
            function (data) {
                if (data.result = 1) {
                    $(".e-nr .clearfix").html('');

                    DoT.exec("/modules/home/template/course.html", function (templateFun) {
                        var innerText = templateFun(data.courses);
                        innerText = $(innerText);
                        $(".e-nr .clearfix").append(innerText);
                    });

                    $("#pager").paginate({
                        total_count: data.total,
                        count: data.pages,
                        start: Course.options.pageIndex,
                        display: 2,
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
                            Course.options.pageIndex = page;
                            Course.getCourses();
                        }
                    });

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
                if (data.result = 1) {
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
        html += '<dd><a href="#">' + item.UserName + '</a></dd>';
        html+='<dd><a href="#">高级金融讲师</a></dd>';
        html += '</dl>';

        return html;
    };

    module.exports= Course;
});