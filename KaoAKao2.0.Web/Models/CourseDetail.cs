using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KaoAKao.Entity;

namespace KaoAKao2._0.Web.Models
{
    public class CourseDetail
    {
       public CourseEntity course;

       public List<LessonEntity> lessons;
    }
}