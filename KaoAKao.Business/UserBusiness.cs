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
        /// <param name="type"></param>
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
        /// 获取等级
        /// </summary>
        /// <param name="type"></param>
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
        /// <param name="keywords"></param>
        /// <param name="pageSize"></param>
        /// <param name="index"></param>
        /// <param name="total"></param>
        /// <param name="pages"></param>
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
        /// <param name="userid"></param>
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
        /// <param name="userid"></param>
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
        /// <param name="userName"></param>
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
        /// <param name="userName"></param>
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
        /// 注册会员
        /// </summary>
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

        //添加会员等级
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
        #endregion

        #region 编辑

        /// <summary>
        /// 编辑教师信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="userName"></param>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="photopath"></param>
        /// <param name="keyWords"></param>
        /// <param name="desc"></param>
        /// <param name="operateIP"></param>
        /// <param name="operateID"></param>
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
        /// <param name="name"></param>
        /// <param name="imgPath"></param>
        /// <param name="desc"></param>
        /// <param name="operateIP"></param>
        /// <param name="operateID"></param>
        /// <returns></returns>
        public bool EditUserLevel(int id, string name, string imgPath, double Discount, string desc, string operateIP, string operateID)
        {
            return new UserDAL().EditUserLevel(id, name, imgPath, Discount, desc, operateIP, operateID);
        }

        #endregion

        #region 删除

        public bool DeleteUser(string userid, string operateIP, string operateID)
        {
            return new UserDAL().DeleteUser(userid, operateIP, operateID);
        }

        #endregion
    }
}
