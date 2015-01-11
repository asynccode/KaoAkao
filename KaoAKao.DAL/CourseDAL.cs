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

        public DataTable GetCourseLessons(string courseid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@CourseID",courseid)
                                   };
            return GetDataTable("select * from Lessons where Status<>9  and CourseID=@CourseID order by Sort,CreateDate", paras, CommandType.Text);
        }

        public DataTable GetCourseLessons(string courseid, string pid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@CourseID",courseid),
                                       new SqlParameter("@PID",pid)
                                   };
            return GetDataTable("select * from Lessons where Status<>9 and CourseID=@CourseID and PID=@PID  order by Sort,CreateDate", paras, CommandType.Text);
        }

        public DataTable GetCourseLessonByID(string lessonid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@LessonID",lessonid)
                                   };
            return GetDataTable("select * from Lessons where LessonID=@LessonID", paras, CommandType.Text);
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

        public object AddCourseLesson(string lessonName, string courseID, string pid,  string keyWords, string description, string videoURL, string videoSize, int sort, string operateIP, string operateID)
        {
            string sql = @" declare @LessonID nvarchar(64) set @LessonID=NEWID() 
                            INSERT INTO Lessons(LessonID,LessonName,CourseID,PID,Status,RadioURL,RadioSize,ViewCount,PraiseCount,ShareCount,CollectCount,Sort,IsHot,CreateDate
                                                 ,LastOperateDate,Keywords,Description,OperateIP,OperateID)
                            values (@LessonID,@LessonName,@CourseID,@PID,1,@RadioURL,@RadioSize,0,0,0,0,@Sort,1,getdate(),getdate(),@Keywords,@Description,@OperateIP,@OperateID)
                            select @LessonID";

            SqlParameter[] paras = { 
                                       new SqlParameter("@LessonName",lessonName),
                                       new SqlParameter("@CourseID",courseID),
                                       new SqlParameter("@PID",pid),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@RadioURL",videoURL),
                                       new SqlParameter("@RadioSize",videoSize),
                                       new SqlParameter("@Sort",sort),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteScalar(sql, paras, CommandType.Text);
        }

        public string AddUserCourse(string userid, string courseid, string lessonid, int type, string operateIP, string operateID, out int result, out string resultdes)
        {
            string id = "";
            SqlParameter[] paras = {
                                       new SqlParameter("@ID",SqlDbType.NVarChar,50),
                                       new SqlParameter("@Result",SqlDbType.Int),
                                       new SqlParameter("@ResultDes",SqlDbType.NVarChar,4000),
                                       new SqlParameter("@UserID",userid),
                                       new SqlParameter("@CourseID",courseid),
                                       new SqlParameter("@Type",type)
                                   };
            paras[0].Direction = ParameterDirection.Output;
            paras[1].Direction = ParameterDirection.Output;
            paras[2].Direction = ParameterDirection.Output;

            ExecuteScalar("P_UserCourseAdd", paras, CommandType.StoredProcedure);
            id = paras[0].Value.ToString();
            result = Convert.ToInt32(paras[1].Value);
            resultdes = paras[2].Value.ToString();
            return id;
        }

        public int AddCourseInteraction(string userid, string courseid, int replyid, string content, int type, double integral, string operateIP, string operateID,out int result)
        {

            int id = 0;
            SqlParameter[] paras = {
                                       new SqlParameter("@ID",SqlDbType.Int),
                                       new SqlParameter("@Result",SqlDbType.Int),
                                       new SqlParameter("@UserID",userid),
                                       new SqlParameter("@CourseID",courseid),
                                       new SqlParameter("@TypeID",type),
                                       new SqlParameter("@Content",content),
                                       new SqlParameter("@ReplyID",replyid),
                                       new SqlParameter("@Integral",integral),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID),
                                   };
            paras[0].Direction = ParameterDirection.Output;
            paras[1].Direction = ParameterDirection.Output;
            paras[2].Direction = ParameterDirection.Output;

            ExecuteScalar("P_UserInteractionAdd", paras, CommandType.StoredProcedure);
            id = Convert.ToInt32(paras[0].Value);
            result = Convert.ToInt32(paras[1].Value);
            return id;
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

        public bool EditCourseLesson(string lessonID, string lessonName, string courseID, string pid, string keyWords, string description, string videoURL, string videoSize, int sort, string operateIP, string operateID)
        {
            string sql = @" update Lessons set LessonName=@LessonName,PID=@PID,KeyWords=@KeyWords,
                            Description=@Description ,RadioURL=@VideoURL,RadioSize=@VideoSize,Sort=@Sort,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID
                            where LessonID=@LessonID";

            SqlParameter[] paras = { 
                                       new SqlParameter("@LessonID",lessonID),
                                       new SqlParameter("@LessonName",lessonName),
                                       new SqlParameter("@PID",pid),
                                       new SqlParameter("@KeyWords",keyWords),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@VideoURL",videoURL),
                                       new SqlParameter("@VideoSize",videoSize),
                                       new SqlParameter("@Sort",sort),
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

        public bool DeleteCourseLessons(string lessonid, string operateIP, string operateID)
        {
            string sql = @" update  Lessons set [Status] = 9,LastOperateDate=GETDATE(),OperateIP=@OperateIP,OperateID=@OperateID 
                            where LessonID=@LessonID";
            SqlParameter[] paras = { 
                                       new SqlParameter("@LessonID",lessonid),
                                       new SqlParameter("@OperateIP",operateIP),
                                       new SqlParameter("@OperateID",operateID)
                                   };
            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }

        #endregion
    }
}
