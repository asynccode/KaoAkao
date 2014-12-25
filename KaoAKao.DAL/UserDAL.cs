using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaoAKao.DAL
{
    public class UserDAL : BaseDAL
    {
        #region 查询

        public DataTable GetUserByUserID(string userid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@UserID",userid)
                                   };
            return GetDataTable("select * from Users where UserID=@UserID", paras, CommandType.Text);
        }


        public DataTable UserLogin(string userName, string loginpwd)
        {

            SqlParameter[] paras = { 
                                       new SqlParameter("@UserName",userName),
                                       new SqlParameter("@LoginPwd",loginpwd)
                                   };
            return GetDataTable("select * from Users where (UserName=@UserName or MobileTele=@UserName or Email=@UserName) and LoginPwd=@LoginPwd ", paras, CommandType.Text);
        }

        public bool IsExistUserName(string userName)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@UserName",userName)
                                   };
            return Convert.ToInt32(ExecuteScalar("select count(0) from Users where UserName=@UserName or MobileTele=@UserName or Email=@UserName ", paras, CommandType.Text)) > 0;
        }

        public bool IsExistUserName(string userID, string userName)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@UserID",userID),
                                       new SqlParameter("@UserName",userName)
                                   };
            return Convert.ToInt32(ExecuteScalar("select count(0) from Users where UserID<>@UserID and (UserName=@UserName or MobileTele=@UserName or Email=@UserName)", paras, CommandType.Text)) > 0;
        }

        #endregion

        #region 添加


        public object AddUsers(string mobile, string email, string loginpwd,int usertype , string operateIP, string operateID)
        {
            string sql = @" declare @UserID nvarchar(64) set @UserID=NEWID() 
                            INSERT INTO Users(UserID,MobileTele,Email,LoginPwd,UserType,RegisterDate,OperateIP,OperateID)
                            values(@UserID,@MobileTele,@Email,@LoginPwd,@UserType,GetDate(),@OperateIP,@OperateID)
                            select @UserID";

            SqlParameter[] paras = { 
                                       new SqlParameter("@MobileTele",mobile),
                                       new SqlParameter("@Email",email),
                                       new SqlParameter("@LoginPwd",loginpwd),
                                       new SqlParameter("@UserType",usertype),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteScalar(sql, paras, CommandType.Text);
        }

        #endregion

        #region 编辑
        #endregion

        #region 删除
        #endregion
    }
}
