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


        public DataTable GetTeachers()
        {
            return GetDataTable("select * from Users where UserType=1 and Status!=9");
        }

        public DataTable GetUserLevelByType(int type)
        {
            return GetDataTable("select * from UserLevel where Type=" + type + " order by Level");
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

        public object AddUsers(string mobile, string email, string loginpwd, int usertype, string operateIP, string operateID)
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
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mobile"></param>
        /// <param name="email"></param>
        /// <param name="loginpwd"></param>
        /// <param name="photoPath"></param>
        /// <param name="usertype"></param>
        /// <param name="keyWords"></param>
        /// <param name="desc"></param>
        /// <param name="operateIP"></param>
        /// <param name="operateID"></param>
        /// <param name="result">返回结果状态</param>
        /// <param name="resultdes">返回结果信息</param>
        /// <returns></returns>
        public string AddUsers(string name, string mobile, string email, string loginpwd, string photoPath, int usertype, string keyWords, string desc, string operateIP, string operateID,out int result,out string resultdes)
        {
            string id = string.Empty;
            SqlParameter[] paras = {
                                       new SqlParameter("@ID",SqlDbType.NVarChar),
                                       new SqlParameter("@Result",SqlDbType.Int),
                                       new SqlParameter("@ResultDes",SqlDbType.NVarChar),
                                       new SqlParameter("@UserName",""),
                                       new SqlParameter("@Name",name),
                                       new SqlParameter("@PetName",""),
                                       new SqlParameter("@LoginPwd",""),
                                       new SqlParameter("@SecurityPwd",""),
                                       new SqlParameter("@LevelID",0),
                                       new SqlParameter("@UserType",0),
                                       new SqlParameter("@Degree",0),
                                       new SqlParameter("@HomeTele",""),
                                       new SqlParameter("@MobileTele",mobile),
                                       new SqlParameter("@Email",email),
                                       new SqlParameter("@Sex",0),
                                       new SqlParameter("@Birthday","1900-01-01"),
                                       new SqlParameter("@AreaCode",""),
                                       new SqlParameter("@Address",""),
                                       new SqlParameter("@PhotoPath",photoPath),
                                       new SqlParameter("@State",0),
                                       new SqlParameter("@Question",""),
                                       new SqlParameter("@Answer",""),
                                       new SqlParameter("@PostCode",""),
                                       new SqlParameter("@PaperTypeCode",""),
                                       new SqlParameter("@PaperNumber",""),
                                       new SqlParameter("@IntegralIn",0),
                                       new SqlParameter("@IntegralOut",0),
                                       new SqlParameter("@GrowValue",0),
                                       new SqlParameter("@ExpValue",0),
                                       new SqlParameter("@PwdErrorTimes",0),
                                       new SqlParameter("@LoginTimes",0),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",desc),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            paras[0].Direction = ParameterDirection.Output;
            paras[1].Direction = ParameterDirection.Output;
            paras[2].Direction = ParameterDirection.Output;

            ExecuteScalar("P_CoursesAdd", paras, CommandType.StoredProcedure);
            id = paras[0].Value.ToString();
            result = Convert.ToInt32(paras[1].Value);
            resultdes = paras[2].Value.ToString();
            return id;

        }
        //添加会员等级
        public int AddUserLevel(int level, string name, int type, int minexp, double discount, string imgpath, string description, string operateIP, string operateID,out int result)
        {
            int id=0;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ID",SqlDbType.Int),
                                       new SqlParameter("@result",SqlDbType.Int),
                                       new SqlParameter("@Level",level),
                                       new SqlParameter("@Name",name),
                                       new SqlParameter("@Type",type),
                                       new SqlParameter("@MinExp",minexp),
                                       new SqlParameter("@Discount",discount),
                                       new SqlParameter("@ImgPath",imgpath),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            paras[0].Direction=ParameterDirection.Output;
            paras[1].Direction=ParameterDirection.Output;

            ExecuteScalar("P_UserLevelAdd", paras, CommandType.StoredProcedure);
            id = Convert.ToInt32(paras[0].Value);
            result = Convert.ToInt32(paras[1].Value);
            return id;
        }
        
        #endregion

        #region 编辑

        public bool EditTeacher(string userid, string userName, string name, string mobile, string email, string photopath, string keyWords, string desc, string operateIP, string operateID)
        {
            string sql = @" Update Users set UserName=@UserName,Name=@Name,MobileTele=@MobileTele,Email=@Email,PhotoPath=@PhotoPath,KeyWords=@KeyWords,
                            Description=@Description,OperateIP=@OperateIP,OperateID=@OperateID where UserID=@UserID";

            SqlParameter[] paras = {
                                       new SqlParameter("@UserName",userName),
                                       new SqlParameter("@Name",name),
                                       new SqlParameter("@MobileTele",mobile),
                                       new SqlParameter("@Email",email),
                                       new SqlParameter("@PhotoPath",photopath),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",desc),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID),
                                       new SqlParameter("@UserID",userid)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion

        #region 删除

        public bool DeleteUser(string userid, string operateIP, string operateID)
        {
            string sql = @"update Users set [Status]= 9 ,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID 
                           where UserID=@UserID";
            SqlParameter[] paras = { 
                                       new SqlParameter("@UserID",userid),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion
    }
}
