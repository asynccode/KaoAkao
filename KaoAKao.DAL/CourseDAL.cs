using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaoAKao.DAL
{
    public class CourseDAL : BaseDAL
    {

        #region 查询

        public DataTable GetCourseCategoryByID(string categoryid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",categoryid)
                                   };
            return GetDataTable("select * from CourseCategory where CategoryID=@CategoryID", paras, CommandType.Text);
        }

        public DataTable GetCourseCategorysByPID(string pid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",pid)
                                   };
            return GetDataTable("select * from CourseCategory where Status!=9 and PID=@CategoryID", paras, CommandType.Text);
        }

        #endregion

        #region 添加

        public bool AddCourseCategoy(string categoryid, string categoryName, string pid, string imgURL, string keyWords, string desc, string operateIP, string operateID)
        {
            string sql = @"INSERT INTO CourseCategory(CategoryID,CategoryName,PID,ImgURL,KeyWords,Description,CreateDate,LastOperateDate,OperateIP,OperateID)
                            VALUES(@CategoryID,@CategoryName,@PID,@ImgURL,@KeyWords,@Description,GETDATE(),GETDATE(),@OperateIP,@OperateID)";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@CategoryName",categoryName),
                                       new SqlParameter("@PID",pid),
                                       new SqlParameter("@ImgURL",imgURL),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",desc),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion

        #region 编辑

        public bool EditCourseCategoy(string categoryid, string categoryName, string pid, string imgURL, string keyWords, string desc, string operateIP, string operateID)
        {
            string sql = @"update CourseCategory set CategoryName=@CategoryName,PID=@PID,ImgURl=@ImgURL,KeyWords=@KeyWords,
                         Description=@Description,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID where CategoryID=@CategoryID";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@CategoryName",categoryName),
                                       new SqlParameter("@PID",pid),
                                       new SqlParameter("@ImgURL",imgURL),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",desc),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion

        #region 删除

        public bool DeleteCourseCategoy(string categoryid, string operateIP, string operateID)
        {
            string sql = @"update CourseCategory set [Status]= 9 ,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID 
                           where CategoryID=@CategoryID or PID=@CategoryID ";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion
    }
}
