using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaoAKao.DAL;
using KaoAKao.Entity;
using KaoAKao.Entity.Enum;
using System.IO;
using System.Web;

namespace KaoAKao.Business
{
    public class UserBusiness
    {
        #region 查询

        /// <summary>
        /// 获取教师列表
        /// </summary>
        /// <returns></returns>
        public static List<Entity.UserEntity> GetTeachers()
        {
            List<Entity.UserEntity> list = new List<Entity.UserEntity>();

            DataTable dt = new UserDAL().GetTeachers();

            foreach (DataRow dr in dt.Rows)
            {
                UserEntity model = new UserEntity();
                model.FillData(dr);
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 获取等级列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static List<Entity.UserLevelEntity> GetUserLevelByType(UserType type)
        {
            List<Entity.UserLevelEntity> list = new List<Entity.UserLevelEntity>();
            DataTable dt = new UserDAL().GetUserLevelByType((int)type);

            foreach (DataRow dr in dt.Rows)
            {
                UserLevelEntity model = new UserLevelEntity();
                model.FillData(dr);
                list.Add(model);
            }

            return list;

        }

        /// <summary>
        /// 获取等级实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public static Entity.UserLevelEntity GetUserLevelByID(int id)
        {
            DataTable dt = new UserDAL().GetUserLevelByID(id);

            UserLevelEntity model = new UserLevelEntity();
            model.FillData(dt.Rows[0]);
            return model;

        }

        /// <summary>
        /// 获取会员列表(分页)
        /// </summary>
        /// <param name="keywords">关键词</param>
        /// <param name="pageSize">页size</param>
        /// <param name="index">页码</param>
        /// <param name="total">返回总记录数</param>
        /// <param name="pages">返回总页数</param>
        /// <returns></returns>
        public static List<Entity.UserEntity> GetUsers(string keywords, int pageSize, int index, UserType type, out int total, out int pages)
        {
            List<Entity.UserEntity> list = new List<Entity.UserEntity>();
            string table = "Users ";
            string columns = " * ";
            StringBuilder build = new StringBuilder();
            build.Append(" Status != 9 and UserType=" + (int)type);

            if (keywords != "")
            {
                build.Append(" and (Name like '%" + keywords + "%' or UserName like '%" + keywords + "%')");
            }

            DataTable dt = CommonBusiness.GetPagerData(table, columns, build.ToString(), "ID", pageSize, index, out total, out pages);

            foreach (DataRow dr in dt.Rows)
            {
                UserEntity model = new UserEntity();
                model.FillData(dr);
                list.Add(model);
            }

            return list;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="userid">会员ID</param>
        /// <returns></returns>
        public static UserEntity GetUserByUserID(string userid)
        {
            DataTable dt = new UserDAL().GetUserByUserID(userid);

            UserEntity model = new UserEntity();
            if (dt.Rows.Count > 0)
            {
                model.FillData(dt.Rows[0]);
            }

            return model;
        }
        /// <summary>
        /// 登录并获取会员信息
        /// </summary>
        /// <param name="userName">用户名、Email、手机</param>
        /// <param name="loginpwd">密码</param>
        /// <returns></returns>
        public static UserEntity UserLogin(string userName, string loginpwd)
        {
            loginpwd = DESEncrypt.GetEncryptionPwd(loginpwd);
            DataTable dt = new UserDAL().UserLogin(userName, loginpwd);

            UserEntity model = null;
            if (dt.Rows.Count > 0)
            {
                model = new UserEntity();
                model.FillData(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 判断是否存在用户名、手机号码、邮箱
        /// </summary>
        /// <param name="userName">用户名、手机号码、邮箱</param>
        /// <returns></returns>
        public static bool IsExistUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            return new UserDAL().IsExistUserName(userName);
        }
        /// <summary>
        /// 判断是否存在用户名、手机号码、邮箱
        /// </summary>
        /// <param name="userid">会员ID</param>
        /// <param name="userName">用户名、手机号码、邮箱</param>
        /// <returns></returns>
        public static bool IsExistUserName(string userID, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            return new UserDAL().IsExistUserName(userID, userName);
        }

        #endregion

        #region 添加

        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="mobile">手机</param>
        /// <param name="email">邮箱</param>
        /// <param name="loginpwd">登录密码</param>
        /// <param name="usertype">类型</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <param name="result">返回状态 1成功 0不成功</param>
        /// <param name="resultdes">返回字符串</param>
        /// <returns></returns>
        public static string AddUsers(string mobile, string email, string loginpwd, UserType usertype, string operateIP, string operateID, out int result, out string resultdes)
        {
            loginpwd = DESEncrypt.GetEncryptionPwd(loginpwd);
            object obj = new UserDAL().AddUsers(mobile, email, loginpwd, (int)usertype, operateIP, operateID, out result, out resultdes);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="mobile">手机</param>
        /// <param name="email">邮箱</param>
        /// <param name="loginpwd">密码</param>
        /// <param name="photoPath">照片</param>
        /// <param name="usertype">类型</param>
        /// <param name="keyWords">职称</param>
        /// <param name="desc">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <param name="result">返回状态 1成功 0不成功</param>
        /// <param name="resultdes">返回字符串</param>
        /// <returns></returns>
        public static string AddUsers(string name, string mobile, string email, string loginpwd, string photoPath, UserType usertype, string keyWords, string desc, string operateIP, string operateID,out int result,out string resultdes)
        {
            loginpwd = DESEncrypt.GetEncryptionPwd(loginpwd);

            if (!string.IsNullOrEmpty(photoPath) && photoPath != "/modules/images/default.png")
            {
                if (photoPath.IndexOf("?") > 0)
                {
                    photoPath = photoPath.Substring(0, photoPath.IndexOf("?"));
                }
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(photoPath));
                photoPath = "/Content/upload_images/" + file.Name;
                file.MoveTo(HttpContext.Current.Server.MapPath(photoPath));
            }

            object obj = new UserDAL().AddUsers(name, mobile, email, loginpwd, photoPath, (int)usertype, keyWords, desc, operateIP, operateID,out result,out resultdes);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加等级
        /// </summary>
        /// <param name="level">等级</param>
        /// <param name="name">名称</param>
        /// <param name="type">类型</param>
        /// <param name="minexp">经验值</param>
        /// <param name="discount">折扣</param>
        /// <param name="photoPath">图片</param>
        /// <param name="description">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <param name="result">返回状态 1成功 0不成功</param>
        /// <returns></returns>
        public static int AddUserLevel(int level, string name, UserType type, int minexp, double discount, string photoPath, string description, string operateIP, string operateID, out int result)
        {
            if (!string.IsNullOrEmpty(photoPath) && photoPath != "/modules/images/default.png")
            {
                if (photoPath.IndexOf("?") > 0)
                {
                    photoPath = photoPath.Substring(0, photoPath.IndexOf("?"));
                }
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(photoPath));
                photoPath = "/Content/upload_images/" + file.Name;
                file.MoveTo(HttpContext.Current.Server.MapPath(photoPath));
            }

            return new UserDAL().AddUserLevel(level, name, (int)type, minexp, discount, photoPath, description, operateIP, operateID, out result);
        }


        /// <summary>
        /// 添加经验值、积分
        /// </summary>
        /// <param name="userid">会员ID</param>
        /// <param name="accountDate">发生日期</param>
        /// <param name="value">发生数值</param>
        /// <param name="type">UserAccountType 类型</param>
        /// <param name="direction">AccountDirection 类型</param>
        /// <param name="sourceType">SourceType 类型</param>
        /// <param name="remark">备注</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <param name="result">返回状态 1成功 0不成功</param>
        /// <param name="resultdes">返回字符串</param>
        /// <returns></returns>
        public bool AddUserAccount(string userid, DateTime accountDate, double value, UserAccountType type, AccountDirection direction, SourceType sourceType, string remark, string operateIP, string operateID, out int result, out string resultdes)
        {
            return new UserDAL().AddUserAccount(userid, accountDate, value, (int)type, (int)direction, (int)sourceType, remark, operateIP, operateID, out result, out resultdes);
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑会员、教师信息
        /// </summary>
        /// <param name="userid">会员ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="name">姓名</param>
        /// <param name="mobile">手机</param>
        /// <param name="email">邮箱</param>
        /// <param name="photoPath">照片</param>
        /// <param name="desc">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <returns></returns>
        public bool EditTeacher(string userid, string userName, string name, string mobile, string email, string photopath, string keyWords, string desc, string operateIP, string operateID)
        {
            if (photopath.IndexOf("?") > 0)
            {
                photopath = photopath.Substring(0, photopath.IndexOf("?"));
            }
            return new UserDAL().EditTeacher(userid, userName, name, mobile, email, photopath, keyWords, desc, operateIP, operateID);
        }

        /// <summary>
        /// 编辑等级信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">名称</param>
        /// <param name="imgPath">图片</param>
        /// <param name="discount">折扣</param>
        /// <param name="description">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作ID</param>
        /// <returns></returns>
        public bool EditUserLevel(int id, string name, string imgPath, double Discount, string desc, string operateIP, string operateID)
        {
            return new UserDAL().EditUserLevel(id, name, imgPath, Discount, desc, operateIP, operateID);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="userid">会员ID</param>
        /// <param name="operateIP">操作人IP</param>
        /// <param name="operateID">操作人ID</param>
        /// <returns></returns>
        public bool DeleteUser(string userid, string operateIP, string operateID)
        {
            return new UserDAL().DeleteUser(userid, operateIP, operateID);
        }

        #endregion
    }
}
