using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaoAKao.Entity.Enum
{
    /// <summary>
    /// 课程排序
    /// </summary>
    public enum CourseOrderBy
    {
        /// <summary>
        /// 添加日期
        /// </summary>
        CreateDate = 1,
        /// <summary>
        /// 浏览数
        /// </summary>
        ViewCount = 2,
        /// <summary>
        /// 点赞数
        /// </summary>
        PraiseCount = 3,
        /// <summary>
        /// 分享数
        /// </summary>
        ShareCount = 4,
        /// <summary>
        /// 收藏数
        /// </summary>
        CollectCount = 5

    }
}
