define(function (require, exports, module) {
    var Global = require("global");
    var DoT = require("dot");

    var CourseDetail = {};
    //基本参数
    CourseDetail.options = {
        ajaxUrl: "/Home/GetLessonsByCid",
        cID: '',
        lID: '',
        rID: '',
        integral:0,
        operateLessonType: 2,
        interactiveType: 1,
        ReplyInteractiveType:2,
        displayType: 1,
        pageIndex:1,
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
            if (type) {
                CourseDetail.options.operateLessonType = parseInt(type);
                CourseDetail.operateLesson();
            }
        });

        //添加评价
        $("#btn_addComment").bind("click", function () {
            CourseDetail.options.interactiveType = 1;
            CourseDetail.addCourseInteraction(null,null,$(this));
        });

        //添加提问
        $("#btn_addAnswer").bind("click", function () {
            CourseDetail.options.interactiveType = 2;
            CourseDetail.addCourseInteraction(null,null,$(this));
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

        //选择悬赏积分
        $(".quiz-reward #ul_cOrder li").bind("click", function (event) {
            $("#ul_cOrder").slideToggle();
            var count=$(this).attr("BindCount");
            $("#s_answer_count").html(count);
        });

        //除去冒泡
        $(".quiz-reward .answer_count").bind("click", function (event) {
            event.stopPropagation();
            $("#ul_cOrder").slideToggle();
        });

        //页面文档点击
        $(document).on('click', function (e) {
            $("#ul_cOrder").slideUp();
        });

        //累加 评论
        $("#btn_againGetUserInteractions").bind("click", function () {
            CourseDetail.getUserInteractions(1);
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
                        var left = 0;
                        if (i < 5 && i > 0)
                            left = i * -60;
                        else {
                            var j = i % 5;
                            left = i * -60;
                        }

                        html += '<div class="p-l-ts" style="left:'+left+'px;">';
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
                    teacherHtml += ' <a href="#"><i><img src="' + teacher.PhotoPath + '" alt=""/></i>' + teacher.PetName + '</a>';
                    teacherHtml += '</div>';
                    if (teacher.Description.length > 69)
                        teacher.Description = teacher.Description.substring(0, 69) + "... " + "<a href='#'>查看详情</a>";
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
                        html += '<div style="cursor:pointer;" class="l" onclick="location.href=\'/course/detail/' + item.CourseID + '\'"><img style="width:160px;height:90px;" src="' + item.ImgURL + '" alt=""/></div>';
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
    CourseDetail.getUserInteractions = function (isAdd) {
        if (!isAdd)
            CourseDetail.options.pageIndex = 1;
        else
            CourseDetail.options.pageIndex = CourseDetail.options.pageIndex + 1;

        Global.AjaxRequest("/Home/getUserInteractions", "post",
        {
            CID: CourseDetail.options.cID,
            InteractiveType: CourseDetail.options.displayType,
            PageIndex:CourseDetail.options.pageIndex
        },
            function (data) {
                if (data.result == 1) {
                    if (CourseDetail.options.displayType == 1) {
                        DoT.exec("/modules/home/template/replycourse.html", function (templateFun) {
                            var innerText = templateFun(data.userInteractions);
                            innerText = $(innerText);
                            $(".comment-main ul[name='ul_comment_main']").append(innerText).fadeIn();

                            CourseDetail.bindReplyEvent();
                        });
                    }
                    else {
                        DoT.exec("/modules/home/template/qacourse.html", function (templateFun) {
                            var innerText = templateFun(data.userInteractions);
                            innerText = $(innerText);
                            $(".quiz-main ul").append(innerText).fadeIn();

                            $(".quiz-main ul input.q-c-btn").unbind().bind("click", function () {
                                var id = $(this).attr("BindID");
                                CourseDetail.options.interactiveType = 2;
                                CourseDetail.addCourseInteraction(id,null,$(this));
                            });

                            $(".q-c-main").unbind().bind("click", function () {
                                if (!$(this).next().is(":visible"))
                                {
                                    CourseDetail.options.displayType = 2;
                                    var id = $(this).attr("BindID");
                                    CourseDetail.getUserInteractionReplysByID(id);
                                }
                                $(this).next().slideToggle();
                            });
                        });
                    }



                }
            });
    };

    //根据ID 获取评论回复、问题答案列表
    CourseDetail.getUserInteractionReplysByID = function (rID) {

        Global.AjaxRequest("/Home/getUserInteractionReplysByID", "post",
        {
            RID: rID,
            PageIndex: 1
        },
            function (data) {
                if (data.result == 1) {
                    if ($("#d_list" + rID + " ul").html())
                        $("#d_list" + rID + " ul").html('');

                    if ($("#d_list" + rID + " ul").html())
                        $("#d_list" + rID + " ul").html('');


                    if (CourseDetail.options.displayType == 1) {
                        DoT.exec("/modules/home/template/replydetailcourse.html", function (templateFun) {
                            var innerText = templateFun(data.userInteractions);
                            innerText = $(innerText);
                            $("#d_list" + rID + " ul").append(innerText).fadeIn();

                            CourseDetail.bindReplyEvent();
                        });
                    }
                    else {
                        $("#li_qa" + rID + " dt[name='dt_qadetailcourse']").remove();
                        $("#li_qa" + rID + " dd[name='dd_qadetailcourse']").remove();

                        DoT.exec("/modules/home/template/qadetailcourse.html", function (templateFun) {
                            var innerText = templateFun(data.userInteractions);
                            innerText = $(innerText);
                            $("#li_qa" + rID + " dl").append(innerText).fadeIn();

                            //评级点赞
                            $("#li_qa" + rID + " a[name='abtn_PraiseCount']").unbind().bind("click", function () {
                                var PraiseCount = parseInt($(this).html());
                                PraiseCount = PraiseCount + 1;
                                $(this).html(PraiseCount);

                                var id = $(this).attr("BindReplyID");
                                CourseDetail.addUserInteraction(id);
                            });
                        });
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
                        var count = $("#s_coursePraiseCount").html();
                        count = parseInt(count);

                        if ($("#txt_isPraiseCourse").val() == "0") {
                            $(".p-f-main .pra i").css("backgroundPosition", "24px -24px");
                            $("#s_coursePraiseCount").html((count + 1));
                            $("#txt_isPraiseCourse").val("1");
                        }
                        else {
                            $(".p-f-main .pra i").css("backgroundPosition", "0px -24px");
                            $("#s_coursePraiseCount").html((count-1));
                            $("#txt_isPraiseCourse").val("0");
                        }
                        
                    }
                    else
                    {
                        if ($("#txt_isFavCourse").val() == "0") {
                            $(".p-f-main .col i").css("backgroundPosition", "24px -48px");
                            $("#txt_isFavCourse").val("1");
                        }
                        else {
                            $(".p-f-main .col i").css("backgroundPosition", "0px -48px");
                            $("#txt_isFavCourse").val("0");
                        }
                    }
                }
                else if (data.result == 0)
                {
                    alert("操作失败");
                }
                else if (data.result == -1)
                {
                    alert("您未登录，请先登录");
                }
            });
    };

    //对课程进行评论或提出问答
    CourseDetail.addCourseInteraction = function (replyID, replyUserName,obj) {
        CourseDetail.options.ajaxUrl = "/home/AddCourseInteraction";

        var btn_value = $(obj).val();
        $(obj).val("提交中...");
        $(obj).attr("disabled", "disabled");

        //获取内容
        var content = '';
        if (CourseDetail.options.interactiveType == 1) {
            CourseDetail.options.integral = 0;
            if (replyID)
                content = $("#txt_replyContent_" + replyID).val();
            else
                content = $("#txt_commentMsg").val();
        }
        else
        {
            CourseDetail.options.integral =parseInt( $("#s_answer_count").html());
            if (replyID)
                content=$("#txt_answerMsg" + replyID).val();
           else
                content = $("#txt_answerMsg").val();
        }


        Global.AjaxRequest(CourseDetail.options.ajaxUrl, "post",
        {
            CID: CourseDetail.options.cID,
            Content: content,
            ReplyID: replyID,
            Integral:CourseDetail.options.integral,
            InteractiveType: CourseDetail.options.interactiveType//interactiveType=1 课程评价；interactiveType=2 课程问答
        },
            function (data) {
                $(obj).val(btn_value);
                $(obj).removeAttr("disabled");

                if (data.result == 1) {
                    var returnData = new Array();
                    var item = {};
                    item.Content = content;
                    item.ID = data.replyID;
                    item.UserID = $("#txt_hiddenUserID").val();
                    item.UserName = $("#txt_hiddenUserName").val();
                    item.PhotoPath = $("#txt_hiddenPhotoPath").val();
                    item.PraiseCount = 0;
                    item.ReplyCount = 0;
                    item.Integral = CourseDetail.options.integral;
                    item.CreateDate = "刚刚";
                    
                    if (replyUserName) {
                        var ReplyEntity = {};
                        ReplyEntity.UserName = replyUserName;
                        item.ReplyEntity = ReplyEntity;
                    }
                    returnData.push(item);
                    
                    if (CourseDetail.options.interactiveType == 1)
                    {
                        if (replyID) {
                            $("#txt_replyContent_" + replyID).val('');
                        }
                        else
                            $("#txt_commentMsg").val('');

                        if (replyID) {
                            DoT.exec("/modules/home/template/replydetailcourse.html", function (templateFun) {
                                var innerText = templateFun(returnData);
                                innerText = $(innerText);
                                if ($("#li_" + replyID).html())
                                    $("#li_" + replyID).parent().prepend(innerText).fadeIn();
                                else
                                    $("#d_list" + replyID).prepend(innerText).fadeIn();

                                CourseDetail.bindReplyEvent();

                            });
                        }
                        else
                        {
                            DoT.exec("/modules/home/template/replycourse.html", function (templateFun) {
                                var innerText = templateFun(returnData);
                                innerText = $(innerText);
                                $(".comment-main ul[name='ul_comment_main']").prepend(innerText).fadeIn();

                                CourseDetail.bindReplyEvent();
                            });
                        }

                    }
                    else if (CourseDetail.options.interactiveType == 2)
                    {
                        if (replyID) {
                            $("#txt_answerMsg" + replyID).val('');
                            DoT.exec("/modules/home/template/qadetailcourse.html", function (templateFun) {
                                var innerText = templateFun(returnData);
                                innerText = $(innerText);
                                $("#li_qa" + replyID + " dl dd[name='dd_firstqa']").after(innerText).fadeIn();

                                //评级点赞
                                $("#li_qa" + replyID + " a[name='abtn_PraiseCount']").unbind().bind("click", function () {
                                    var id = $(this).attr("BindReplyID");
                                    var PraiseCount = parseInt($(this).html());
                                    PraiseCount = PraiseCount + 1;
                                    $(this).html(PraiseCount);
                                    CourseDetail.addUserInteraction(id);
                                });
                            });
                        }
                        else {
                            $("#txt_answerMsg").val('');
                            DoT.exec("/modules/home/template/qacourse.html", function (templateFun) {
                                var innerText = templateFun(returnData);
                                innerText = $(innerText);
                                $(".quiz-main ul").prepend(innerText).fadeIn();

                                $(".quiz-main ul input.q-c-btn").unbind().bind("click", function () {
                                    var id = $(this).attr("BindID");
                                    CourseDetail.options.interactiveType = 2;
                                    CourseDetail.addCourseInteraction(id,$(this));
                                });

                                $(".q-c-main").unbind().bind("click", function () {
                                    if (!$(this).next().is(":visible")) {
                                        CourseDetail.options.displayType = 2;
                                        var id = $(this).attr("BindID");
                                        CourseDetail.getUserInteractionReplysByID(id);
                                    }
                                    $(this).next().slideToggle();
                                });

                            });
                        }
                    }

                }
                else if (data.result == 0) {
                    alert("操作失败");
                }
                else if (data.result == -1) {
                    alert("您未登录，请先登录");
                }
            });
    };

    //显示评价列表的div
    CourseDetail.showReplyDiv = function (replyID) {
        var divDisplay = "#child" + replyID;
        var d_list = "#d_list" + replyID;

        if ( $(divDisplay).is(":visible") ) {
            $(divDisplay).hide();
            $(d_list).hide();
        }
        else {
            CourseDetail.options.displayType = 1;
            CourseDetail.getUserInteractionReplysByID(replyID);
            $(divDisplay).show();
            $(d_list).show();
        }
    }

    //添加评论、提问收藏、点赞
    CourseDetail.addUserInteraction = function (replyID) {
        CourseDetail.options.ajaxUrl = "/home/addUserInteraction";

        Global.AjaxRequest(CourseDetail.options.ajaxUrl, "post",
        {
            RID: replyID,
            ReplyInteractiveType: CourseDetail.options.ReplyInteractiveType
        },
            function (data) {
                if (data.result == 0) {
                    alert("操作失败");
                }
                else if (data.result == -1) {
                    alert("您未登录，请先登录");
                }
            });
    };

    //绑定评级回复相关事件
    CourseDetail.bindReplyEvent = function () {
        //评价回复
        $(".comment-main ul input[name='btn_replyComment']").unbind().bind("click", function () {
            var id = $(this).attr("BindReplyID");
            var replyUserName = $(this).attr("BindReplyUserName");
            CourseDetail.options.interactiveType == 1;
            CourseDetail.addCourseInteraction(id, replyUserName, $(this));
        });

        //评价输入框
        $(".comment-main ul a[name='a_showReplyDiv']").unbind().bind("click", function () {
            var id = $(this).attr("BindReplyID");
            CourseDetail.showReplyDiv(id);
        });

        //评级点赞
        $(".comment-main ul a[name='abtn_PraiseCount']").unbind().bind("click", function () {
            var PraiseCount = parseInt($(this).find("span").html());
            PraiseCount = PraiseCount + 1;
            $(this).find("span").html(PraiseCount);

            var id = $(this).attr("BindReplyID");
            CourseDetail.addUserInteraction(id);
        });
    };

    module.exports = CourseDetail;
});
