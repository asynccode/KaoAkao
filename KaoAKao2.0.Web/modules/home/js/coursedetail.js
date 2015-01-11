define(function (require, exports, module) {
    var Global = require("global");

    var CourseDetail = {};
    //基本参数
    CourseDetail.options = {
        ajaxUrl: "/Home/GetLessonsByCid",
        cID: '',
        lID: '',
        operateLessonType:2,
        lessonsData:null
    };

    //初始化
    CourseDetail.init = function () {
        CourseDetail.bindEvent();

        CourseDetail.getLessonsByCid();

        CourseDetail.getOtherCourses();
    }

    //绑定事件
    CourseDetail.bindEvent = function () {
        $(".play-foot .p-f-main a").bind("click", function () {
            var type = $(this).attr("BindType");
            CourseDetail.options.operateLessonType = parseInt(type);
            CourseDetail.operateLesson();
        });
    }

    //根据课程id获取课程章节列表
    CourseDetail.getLessonsByCid=function()
    {
        CourseDetail.options.ajaxUrl = "/home/GetLessonsByCid";
        CourseDetail.options.cID = $("#txt_courseID").val();
        CourseDetail.options.tID = $("#txt_teacherID").val();

        Global.AjaxRequest(CourseDetail.options.ajaxUrl, "post", { CID: CourseDetail.options.cID, TID: CourseDetail.options.tID },
            function (data) {
                if (data.result == 1)
                {
                    CourseDetail.options.lessonsData=data.lessons;
                    var len = data.lessons.length;
                    //课程章节列表
                    for (var i = 0; i < len; i++)
                    {
                        var item = data.lessons[i];
                        var html = '';
                        if (i == 0)
                            html += '<a href="javascript:void(0);" class="active" BindIndex="'+i+'">'+(i+1)+'';
                        else
                            html += '<a href="javascript:void(0);" BindIndex="' + i + '">' + (i + 1) + '';
                        html += '<div class="p-l-ts">';
                        html += '<h3>大纲简介</h3>';
                        html += '<p>'+item.Description+'</p>';
                        html += '</div>';
                        html += ' <div class="arrow-up"></div>';
                        html += '</a>';
                        $(".p-l-down div.p-l-inner").append(html);
                    }

                    //课程章节绑定点击事件
                    $(".p-l-down div.p-l-inner a").bind("click", function () {
                        $(".p-l-down div.p-l-inner a").removeClass("active");
                        $(this).addClass("active");

                        var index = $(this).attr("BindIndex");
                        index = parseInt(index);
                        var item = CourseDetail.options.lessonsData[index];
                        CourseDetail.fillCourseDetail(item,index);
                    });

                    //填充课程章节内容
                    CourseDetail.fillCourseDetail(data.lessons[0], 0);

                    //填充课程教师信息
                    var teacher=data.teacher;
                    var teacherHtml = '<div class="p-l-name">';
                    teacherHtml += ' <a href="#"><i><img src="/modules/home/images/index_16.jpg" alt=""/></i>' + teacher.UserName + '</a>';
                    teacherHtml += '</div>';
                    teacherHtml += '<div class="p-l-brief">' + teacher.Description + '</div>';
                    $(".play-container div.p-l-up").append(teacherHtml);
           
          
          
                }
            });
    }

    //获取相关的课程列表
    CourseDetail.getOtherCourses = function () {
        Global.AjaxRequest("/Home/GetGoodCourses", "post",
        null,
            function (data) {
                if (data.result = 1) {
                    var len = data.courses.length;
                    for (var i = 0; i < len; i++)
                    {
                        var item=data.courses[i];
                        var html = '';
                        html += '<dd class="clearfix">';
                        html += '<div class="l"><img src="/modules/home/images/play_02.jpg" alt=""/></div>';
                        html += '<div class="r">';
                        html += '<h3>' + item.Keywords + '</h3>';
                        html += '<p>' + item.CourseName + '</p>';
                        html += '</div>';
                        html += '</dd>';

                        $(".l-c-right .correlate").append(html);
                    }
                }
            });
    };

    //填充课程章节内容
    CourseDetail.fillCourseDetail = function (item,index) {
        $("#s_lessonCountE").html("P" + (index + 1));
        $("#s_lessonCountZ").html("(" + (index + 1) + ")");

        $("#s_lessonPraiseCount").html(item.PraiseCount);
        $("#txt_LessonID").val(item.LessonID);
    };

    //操作课程章节 点赞、喜欢、分享
    CourseDetail.operateLesson = function () {
        CourseDetail.options.ajaxUrl = "/home/OperateLesson";
        CourseDetail.options.lID = $("#txt_LessonID").val();
        Global.AjaxRequest(CourseDetail.options.ajaxUrl, "post",
        {
            CID: CourseDetail.options.cID,
            lID: CourseDetail.options.lID,
            Type: CourseDetail.options.operateLessonType
        },
            function (data) {
                if (data.result = 1) {
                    alert("操作成功");
                }
            });
    };

    module.exports = CourseDetail;
});
