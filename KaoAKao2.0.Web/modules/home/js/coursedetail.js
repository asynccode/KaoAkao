define(function (require, exports, module) {
    var Global = require("global");
    var DoT = require("dot");

    var CourseDetail = {};
    //基本参数
    CourseDetail.options = {
        ajaxUrl: "/Home/GetLessonsByCid",
        cID: '',
        lID: '',
        operateLessonType: 2,
        interactiveType: 1,
        displayType: 1,
        lessonsData:null
    };

    //初始化
    CourseDetail.init = function () {
        CourseDetail.bindEvent();

        CourseDetail.getLessonsByCid();

        CourseDetail.getOtherCourses();

        CourseDetail.getUserInteractions();
    }

    //绑定事件
    CourseDetail.bindEvent = function () {
        //点赞和收藏
        $(".play-foot .p-f-main a").bind("click", function () {
            var type = $(this).attr("BindType");
            CourseDetail.options.operateLessonType = parseInt(type);
            CourseDetail.operateLesson();
        });

        //添加评价
        $("#btn_addComment").bind("click", function () {
            CourseDetail.options.interactiveType = 1;
            CourseDetail.addCourseInteraction();
        });

        //添加提问
        $("#btn_addAnswer").bind("click", function () {
            CourseDetail.options.interactiveType = 2;
            CourseDetail.addCourseInteraction();
        });

        //评价菜单栏点击
        $("#d_comment").bind("click", function () {
            CourseDetail.options.displayType = 1;

            CourseDetail.getUserInteractions();
        });

        //提问菜单栏点击
        $("#d_answer").bind("click", function () {
            CourseDetail.options.displayType = 2;

            CourseDetail.getUserInteractions();
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
                if (data.result == 1) {
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

    //获取课程的评价或问题列表
    CourseDetail.getUserInteractions = function () {

        Global.AjaxRequest("/Home/getUserInteractions", "post",
        {
            CID: CourseDetail.options.cID,
            InteractiveType: CourseDetail.options.displayType,
            PageIndex:1
        },
            function (data) {
                if (data.result == 1) {
                    if (CourseDetail.options.displayType == 1) {
                        $(".comment-main ul").html('');
                    }
                    else {
                        $(".quiz-main ul").html('');
                    }

                    var len = data.userInteractions.length;
                    for (var i = 0; i < len; i++)
                    {
                        var item = data.userInteractions[i];
                        if (CourseDetail.options.displayType == 1) {
                            DoT.exec("/modules/home/template/replycourse.html", function (templateFun) {
                                var innerText = templateFun(item);
                                innerText = $(innerText);
                                $(".comment-main ul").append(innerText).fadeIn();

                                $("#btn_replyComment_" + item.ID).bind("click", function () {
                                    CourseDetail.options.interactiveType == 1;
                                    CourseDetail.addCourseInteraction(item.ID);
                                });

                            });
                        }
                        else if (CourseDetail.options.displayType == 2) {
                            DoT.exec("/modules/home/template/qacourse.html", function (templateFun) {
                                var innerText = templateFun(item);
                                innerText = $(innerText);
                                $(".quiz-main ul").append(innerText).fadeIn();
                            });
                        }

                    }
                }
            });
    };

    //填充课程章节基本信息
    CourseDetail.fillCourseDetail = function (item,index) {
        $("#s_lessonCountE").html("P" + (index + 1));
        $("#s_lessonCountZ").html("(" + (index + 1) + ")");

        $("#s_lessonName").html(item.LessonName);
        $("#d_lessonDes").html(item.Description);
        $("#txt_LessonID").val(item.LessonID);

        var player = polyvObject('#lesson_playBox').videoPlayer({
            'width': '860',
            'height': '500',
            'vid': item.RadioURL
        });
    };

    //操作课程章节 点赞、喜欢、分享
    CourseDetail.operateLesson = function () {
        CourseDetail.options.ajaxUrl = "/home/OperateLesson";
        CourseDetail.options.lID = $("#txt_LessonID").val();

        Global.AjaxRequest(CourseDetail.options.ajaxUrl, "post",
        {
            CID: CourseDetail.options.cID,
            lID: CourseDetail.options.lID,
            OperateLessonType: CourseDetail.options.operateLessonType
        },
            function (data) {
                if (data.result == 1) {
                    if (CourseDetail.options.operateLessonType == 2) {
                        $(".p-f-main .pra i").css("backgroundPosition", "24px -24px");
                        var count = $("#s_coursePraiseCount").html();
                        count = parseInt(count);
                        $("#s_coursePraiseCount").html((count + 1));
                    }
                    else {
                        $(".p-f-main .col i").css("backgroundPosition", "24px -48px");
                    }
                }
                else if (data.result == 0)
                {
                    alert("操作失败");
                }
                else if (data.result == -1)
                {
                    alert("您未登录，请先登录");
                    setTimeout(function () { location.href="/home/login"}, 1000);
                }
            });
    };

    //对课程进行评论或提出问答
    CourseDetail.addCourseInteraction = function (replyID) {
        CourseDetail.options.ajaxUrl = "/home/AddCourseInteraction";

        var content = '';
        if (CourseDetail.options.interactiveType == 1) {
            if(replyID) 
                content = $("#txt_replyContent_" + replyID).val();
            else
                content = $("#txt_commentMsg").val();
        }
        else {
            content = $("#txt_answerMsg").val();
        }

        Global.AjaxRequest(CourseDetail.options.ajaxUrl, "post",
        {
            CID: CourseDetail.options.cID,
            Content:content,
            InteractiveType: CourseDetail.options.interactiveType
        },
            function (data) {
                if (data.result == 1) {
                    var item = {};
                    item.Content = content;
                    item.ID = data.replyID;
                    if (CourseDetail.options.interactiveType == 1)
                    {
                        if (replyID)
                            $("#txt_replyContent_" + replyID).val('');
                        else
                            $("#txt_commentMsg").val('');
                        $("#child" + replyID).hide();
                            
                        DoT.exec("/modules/home/template/replycourse.html", function (templateFun) {
                            var innerText = templateFun(item);
                            innerText = $(innerText);
                            $(".comment-main ul").prepend(innerText).fadeIn();

                            $("#btn_replyComment_" + item.ID).bind("click", function () {
                                CourseDetail.options.interactiveType == 1;
                                CourseDetail.addCourseInteraction(item.ID);
                            });

                        });
                    }
                    else if (CourseDetail.options.interactiveType == 2)
                    {
                        $("#txt_answerMsg").val('');
                        DoT.exec("/modules/home/template/qacourse.html", function (templateFun) {
                            var innerText = templateFun(item);
                            innerText = $(innerText);
                            $(".quiz-main ul").prepend(innerText).fadeIn();
                        });
                    }

                }
                else if (data.result == 0) {
                    alert("操作失败");
                }
                else if (data.result == -1) {
                    alert("您未登录，请先登录");
                    setTimeout(function () { location.href = "/home/login" }, 1000);
                }
            });
    };

    module.exports = CourseDetail;
});
