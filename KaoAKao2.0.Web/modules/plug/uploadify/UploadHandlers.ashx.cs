using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KaoAKao.Web.modules.plug.uploadify
{
    /// <summary>
    /// UploadHandlers 的摘要说明
    /// </summary>
    public class UploadHandlers : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];

            //图片文件夹路径
            string folder = context.Request["folder"];
            //文件夹绝对路径
            string uploadPath = HttpContext.Current.Server.MapPath(context.Request["folder"]);
            //更换前路径
            string oldPath = context.Request["old"];
            //if (oldPath != null && oldPath != "/module/images/default.png" && oldPath.IndexOf("/module/plug/uploadify/tempFiles") >= 0)
            //{
            //    oldPath = HttpContext.Current.Server.MapPath(oldPath);
            //    if (File.Exists(oldPath))
            //    {
            //        File.Delete(oldPath);
            //    }
            //}

            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (string.IsNullOrEmpty(oldPath) || oldPath == "/modules/images/default.png")
                {
                    string[] arr = file.FileName.Split('.');
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssms") + new Random().Next(1000, 9999).ToString() + "." + arr[arr.Length - 1];
                    string filePath = uploadPath + fileName;
                    file.SaveAs(filePath);

                    context.Response.Write(folder + fileName);
                }
                else
                {
                    file.SaveAs(HttpContext.Current.Server.MapPath(oldPath));
                    context.Response.Write(oldPath);
                }
            }
            else
            {
                context.Response.Write("0");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}