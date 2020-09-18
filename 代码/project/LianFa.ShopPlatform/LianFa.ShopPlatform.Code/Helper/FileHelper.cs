using System.Collections;
using System.IO;
using System.Web;
using LianFa.ShopPlatform.Code.Enums;

namespace LianFa.ShopPlatform.Code.Helper
{
    public static class CustomFileHelper
    {
        /// <summary>
        /// 拷贝文件
        /// </summary>
        public static Hashtable MoveToFiles(string src, string destFolder)
        {
            var hash = new Hashtable
            {
                ["Code"] = ResponseCode.UploadFail,
            };
            destFolder = HttpContext.Current.Server.MapPath(destFolder);
            FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(src));
            if (file.Exists)
            {
                //不存在目录则创建
                if (!Directory.Exists(destFolder))
                {
                    if (!string.IsNullOrEmpty(destFolder))
                    {
                        Directory.CreateDirectory(destFolder);
                    }
                }
                file.MoveTo(Path.Combine(destFolder, file.Name)); //复制 ，剪切的话file.MoveTo();  
                hash["FileExt"] = file.Extension;
                hash["FileName"] = file.Name;
                hash["FileSize"] = file.Length;
                hash["Code"] = (int)ResponseCode.UploadSuccess;
            }
            return hash;
        }
    }
}
