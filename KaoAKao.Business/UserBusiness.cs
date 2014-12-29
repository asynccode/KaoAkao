using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KaoAKao.DAL;
using KaoAKao.Entity;
using KaoAKao.Entity.Enum;

namespace KaoAKao.Business
{
    public class UserBusiness
    {
        #region 查询

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

        #endregion

        #region 添加

        /// <summary>
        /// 注册会员
        /// </summary>
        public static string AddUsers(string mobile, string email, string loginpwd, UserType usertype,  string operateIP, string operateID)
        {
            loginpwd = DESEncrypt.GetEncryptionPwd(loginpwd);
            object obj = new UserDAL().AddUsers(mobile, email, loginpwd, (int)usertype, operateIP, operateID);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return string.Empty;
        }

        //添加会员等级
        public static string AddUserLevel(int level, string name, UserType type, int minexp, double discount, string imgpath, string description, string operateIP, string operateID)
        {
            object obj = new UserDAL().AddUserLevel(level, name, (int)type, minexp, discount, imgpath,description,operateIP,operateID);
            if (obj != null && obj != DBNull.Value)
            {
                return obj.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region 编辑
        #endregion

        #region 删除
        #endregion
    }
}
