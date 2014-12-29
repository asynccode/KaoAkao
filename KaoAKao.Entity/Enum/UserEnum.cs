using System;
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
        //会员
        User = 0,
        //教师
        Teacher = 1
    }
    /// <summary>
    /// 用户-课程类型
    /// </summary>
    public enum UserCoursesType
    {
        //浏览
        Browse = 0,
        /// <summary>
        /// 收藏
        /// </summary>
        Collect = 1,
        /// <summary>
        /// 取消收藏
        /// </summary>
        CancelCollect = 2
    }
}
