﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaoAKao.Entity.Enum
{
    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// 会员
        /// </summary>
        User = 0,

        /// <summary>
        /// 教师
        /// </summary>
        Teacher = 1
    }
    /// <summary>
    /// 用户-课程类型
    /// </summary>
    public enum UserCourseType
    {
        
        /// <summary>
        /// 收藏
        /// </summary>
        Collect = 1,
        /// <summary>
        /// 点赞
        /// </summary>
        Praise = 2,
        /// <summary>
        /// 分享
        /// </summary>
        Share=3,
        /// <summary>
        /// 浏览
        /// </summary>
        Browse = 4
    }
    /// <summary>
    /// 账户类型
    /// </summary>
    public enum UserAccountType
    {
        /// <summary>
        /// 积分
        /// </summary>
        Integral=1,
        /// <summary>
        /// 积分
        /// </summary>
        Growth = 2,
        /// <summary>
        /// 积分
        /// </summary>
        Exp = 3,
    }

    /// <summary>
    /// 积分来源类型
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// 注册
        /// </summary>
        Register = 1,
        /// <summary>
        /// 登录
        /// </summary>
        Login = 2
    }

    /// <summary>
    /// 账户流向
    /// </summary>
    public enum AccountDirection
    {
        /// <summary>
        /// 流入
        /// </summary>
        In = 1,
        /// <summary>
        /// 流出
        /// </summary>
        Out = 2
    }

    /// <summary>
    /// 互动类型
    /// </summary>
    public enum InteractiveType
    {
        /// <summary>
        /// 评论
        /// </summary>
        Review=1,

        /// <summary>
        /// 问答
        /// </summary>
        Answers=2
    }

}
