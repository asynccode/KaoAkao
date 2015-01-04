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

        public DataTable GetCourseByID(string courseid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@CourseID",courseid)
                                   };
            return GetDataTable("select c.*,cc.PID,cc.CategoryName CName from Courses c join CourseCategory cc on c.CategoryID=cc.CategoryID where CourseID=@CourseID", paras, CommandType.Text);
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

        public object AddCourse(string courseName, string cid, string courseImg, double price, string teacherid, int isHot, int limitLevel, string keyWords, string desc, string operateIP, string operateID)
        {
            string sql = @" declare @CourseID nvarchar(64) set @CourseID=NEWID() 
                            INSERT INTO Courses(CourseID,CourseName,CategoryID,ImgURL,Price,TeacherID,IsHot,Status ,LimitLevel,KeyWords,Description,CreateDate,LastOperateDate,OperateIP,OperateID)
                            values(@CourseID,@CourseName,@CategoryID,@ImgURL,@Price,@TeacherID ,@IsHot,1,@LimitLevel,@KeyWords,@Description,GETDATE(),GETDATE(),@OperateIP,@OperateID)
                            select @CourseID";

            SqlParameter[] paras = { 
                                       new SqlParameter("@CourseName",courseName),
                                       new SqlParameter("@CategoryID",cid),
                                       new SqlParameter("@ImgURL",courseImg),
                                       new SqlParameter("@Price",price),
                                       new SqlParameter("@TeacherID",teacherid),
                                       new SqlParameter("@IsHot",isHot),
                                       new SqlParameter("@LimitLevel",limitLevel),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",desc),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteScalar(sql, paras, CommandType.Text);
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

        public bool EditCourse(string courseid, string courseName, string cid, string courseImg, double price, string teacherid, int isHot, int limitLevel, string keyWords, string desc, string operateIP, string operateID)
        {
            string sql = @" update Courses set CourseName=@CourseName,CategoryID=@CategoryID,ImgURL=@ImgURL,Price=@Price,TeacherID=@TeacherID,IsHot=@IsHot ,LimitLevel=@LimitLevel,KeyWords=@KeyWords,
                            Description=@Description,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID
                            where CourseID=@CourseID";

            SqlParameter[] paras = { 
                                       new SqlParameter("@CourseID",courseid),
                                       new SqlParameter("@CourseName",courseName),
                                       new SqlParameter("@CategoryID",cid),
                                       new SqlParameter("@ImgURL",courseImg),
                                       new SqlParameter("@Price",price),
                                       new SqlParameter("@TeacherID",teacherid),
                                       new SqlParameter("@IsHot",isHot),
                                       new SqlParameter("@LimitLevel",limitLevel),
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

        public bool DeleteCourse(string courseid, string operateIP, string operateID)
        {
            string sql = @"update Courses set [Status]= 9 ,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID 
                           where CourseID=@CourseID ";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CourseID",courseid),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion
    }
}
