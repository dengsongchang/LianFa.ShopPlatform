using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HuCheng.Util.Core.Extension;
using HuCheng.Util.Core.Helper;
using HuCheng.Util.Core.Logs;
using LianFa.ShopPlatform.Code.Enums;
using LianFa.ShopPlatform.Code.Image;

namespace LianFa.ShopPlatform.WebApi.Core
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class UploadFile
    {
        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILog ApiLogger = Bootstrapper.GetFromFac<ILog>();

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="saveTempPath"></param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public Task<Hashtable> ImgUpload(HttpContent content, string saveTempPath)
        {
            // 检查是否是 multipart/form-data 
            if (!content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //文件保存目录路径 
            var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);

            //不存在目录则创建
            if (!Directory.Exists(dirTempPath))
            {
                if (!string.IsNullOrEmpty(dirTempPath))
                {
                    Directory.CreateDirectory(dirTempPath);
                }
            }

            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            var task = content.ReadAsMultipartAsync(provider).
                ContinueWith(o =>
                {
                    var hash = new Hashtable
                    {
                        ["Code"] = (int)ResponseCode.UploadFail,
                        ["Message"] = ResponseCode.UploadFail.GetDescription()
                    };
                    var file = provider.FileData[0];//provider.FormData 
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    var fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小 
                    int maxSize = 20000000;
                    if (fileinfo.Length <= 0)
                    {
                        hash["Code"] = (int)ResponseCode.PleaseSelectFile;
                        hash["Message"] = ResponseCode.PleaseSelectFile.GetDescription();
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        hash["Code"] = (int)ResponseCode.UploadFileTooBig;
                        hash["Message"] = ResponseCode.UploadFileTooBig.GetDescription();
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        string fileTypes = "jpg,jpeg,png";
                        if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            hash["Code"] = (int)ResponseCode.UnSupportFileType;
                            hash["Message"] = ResponseCode.UnSupportFileType.GetDescription();
                        }
                        else
                        {
                            var newFileName = DateTimeHelper.GetBeiJingNowDateTime().ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            var uploadFileName = saveTempPath + newFileName + fileExt;
                            if (dirTempPath != null)
                            {
                                fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
                            }
                            fileinfo.Delete();
                            hash["Code"] = (int)ResponseCode.UploadSuccess;
                            hash["Message"] = ResponseCode.UploadSuccess.GetDescription();
                            hash["Data"] = uploadFileName;
                        }
                    }

                    //失败记录异常
                    if (o.IsFaulted && o.Exception != null)
                    {
                        ApiLogger.Error("HeadImgUpload", $"异步上传图像失败,错误{o.Exception.GetBaseException().Message}", o.Exception);
                    }

                    return hash;
                });
            return task;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="content"></param>
        /// <param name="saveTempPath"></param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public Task<Hashtable> WorkImgUpload(HttpContent content, string saveTempPath)
        {
            // 检查是否是 multipart/form-data 
            if (!content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //文件保存目录路径 
            var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);

            //不存在目录则创建
            if (!Directory.Exists(dirTempPath))
            {
                if (!string.IsNullOrEmpty(dirTempPath))
                {
                    Directory.CreateDirectory(dirTempPath);
                }
            }

            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            var task = content.ReadAsMultipartAsync(provider).
                ContinueWith(o =>
                {
                    var hash = new Hashtable
                    {
                        ["Code"] = (int)ResponseCode.UploadFail,
                        ["Message"] = ResponseCode.UploadFail.GetDescription()
                    };
                    var file = provider.FileData[0];//provider.FormData 
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    var fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小 
                    int maxSize = 20000000;
                    if (fileinfo.Length <= 0)
                    {
                        hash["Code"] = (int)ResponseCode.PleaseSelectFile;
                        hash["Message"] = ResponseCode.PleaseSelectFile.GetDescription();
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        hash["Code"] = (int)ResponseCode.UploadFileTooBig;
                        hash["Message"] = ResponseCode.UploadFileTooBig.GetDescription();
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        string fileTypes = "jpg,jpeg,png";
                        if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            hash["Code"] = (int)ResponseCode.UnSupportFileType;
                            hash["Message"] = ResponseCode.UnSupportFileType.GetDescription();
                        }
                        else
                        {
                            var newFileName = DateTimeHelper.GetBeiJingNowDateTime().ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            var uploadFileName = saveTempPath + $"{newFileName}_d" + fileExt;
                            var thumbnailFileName = saveTempPath + $"{newFileName}_s" + fileExt;
                            if (dirTempPath != null)
                            {
                                var dUrl = Path.Combine(dirTempPath, $"{newFileName}_d" + fileExt);
                                var sUrl = Path.Combine(dirTempPath, $"{newFileName}_s" + fileExt);
                                fileinfo.CopyTo(dUrl, true);

                                //压缩图片
                                ImageCompressHelper.CompressImage(dUrl, sUrl);
                            }
                            fileinfo.Delete();
                            hash["Code"] = (int)ResponseCode.UploadSuccess;
                            hash["Message"] = ResponseCode.UploadSuccess.GetDescription();
                            hash["Data"] = uploadFileName;
                            hash["ThumbnailData"] = thumbnailFileName;
                        }
                    }

                    //失败记录异常
                    if (o.IsFaulted && o.Exception != null)
                    {
                        ApiLogger.Error("HeadImgUpload", $"异步上传图像失败,错误{o.Exception.GetBaseException().Message}", o.Exception);
                    }

                    return hash;
                });
            return task;
        }

        /// <summary>
        /// 上传图片(非异步)
        /// </summary>
        /// <param name="content">商品图片</param>
        /// <param name="saveTempPath">保存图片路径</param>
        /// <returns></returns>
        public string UploadImage(HttpContent content, string saveTempPath)
        {
            if (HttpContext.Current.Request.Files.Count <= 0)
                return "-1";

            var image = HttpContext.Current.Request.Files[0];

            var fileName = image.FileName;
            var extension = Path.GetExtension(fileName);

            var dirPath = FileHelper.MapPath(saveTempPath);
            var newFileName = $"pe_{DateTime.Now:yyyyMMddHHmmss_ffff}{extension}";//生成文件名

            var sourceDirPath = dirPath + "source/";
            if (!Directory.Exists(sourceDirPath))
                Directory.CreateDirectory(sourceDirPath);

            var sourcePath = sourceDirPath + newFileName;
            image.SaveAs(sourcePath);

            var path = dirPath + newFileName;

            image.SaveAs(path);

            return newFileName;
        }


        ///// <summary>
        ///// 上传图片
        ///// </summary>
        ///// <param name="content"></param>
        ///// <param name="saveTempPath"></param>
        ///// <returns></returns>
        ///// <exception cref="HttpResponseException"></exception>
        //public Task<Hashtable> ImgUpload(HttpContent content, string saveTempPath)
        //{
        //    // 检查是否是 multipart/form-data 
        //    if (!content.IsMimeMultipartContent("form-data"))
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

        //    //文件保存目录路径 
        //    var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);

        //    //不存在目录则创建
        //    if (!Directory.Exists(dirTempPath))
        //    {
        //        if (!string.IsNullOrEmpty(dirTempPath))
        //        {
        //            Directory.CreateDirectory(dirTempPath);
        //        }
        //    }

        //    // 设置上传目录 
        //    var provider = new MultipartFormDataStreamProvider(dirTempPath);
        //    var task = content.ReadAsMultipartAsync(provider).
        //        ContinueWith(o =>
        //        {
        //            var hash = new Hashtable
        //            {
        //                ["Code"] = (int)ResponseCode.UploadFail,
        //                ["Message"] = ResponseCode.UploadFail.GetDescription()
        //            };
        //            var file = provider.FileData[0];//provider.FormData 
        //            string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
        //            var fileinfo = new FileInfo(file.LocalFileName);
        //            //最大文件大小 
        //            int maxSize = 10000000;
        //            if (fileinfo.Length <= 0)
        //            {
        //                hash["Code"] = (int)ResponseCode.PleaseSelectFile;
        //                hash["Message"] = ResponseCode.PleaseSelectFile.GetDescription();
        //            }
        //            else if (fileinfo.Length > maxSize)
        //            {
        //                hash["Code"] = (int)ResponseCode.UploadFileTooBig;
        //                hash["Message"] = ResponseCode.UploadFileTooBig.GetDescription();
        //            }
        //            else
        //            {
        //                string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
        //                //定义允许上传的文件扩展名 
        //                string fileTypes = "gif,jpg,jpeg,png,bmp";
        //                if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
        //                {
        //                    hash["Code"] = (int)ResponseCode.UnSupportFileType;
        //                    hash["Message"] = ResponseCode.UnSupportFileType.GetDescription();
        //                }
        //                else
        //                {
        //                    var newFileName = DateTimeHelper.GetBeiJingNowDateTime().ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        //                    var uploadFileName = saveTempPath + newFileName + fileExt;
        //                    if (dirTempPath != null)
        //                    {
        //                        fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
        //                    }
        //                    fileinfo.Delete();
        //                    hash["Code"] = (int)ResponseCode.UploadSuccess;
        //                    hash["Message"] = ResponseCode.UploadSuccess.GetDescription();
        //                    hash["Data"] = uploadFileName;
        //                }
        //            }

        //            //失败记录异常
        //            if (o.IsFaulted && o.Exception != null)
        //            {
        //                ApiLogger.Error("HeadImgUpload", $"异步上传图像失败,错误{o.Exception.GetBaseException().Message}", o.Exception);
        //            }

        //            return hash;
        //        });
        //    return task;
        //}

        /// <summary>
        /// 上传excel文件
        /// </summary>
        /// <param name="content"></param>
        /// <param name="saveTempPath"></param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public Task<Hashtable> ExcelUploasd(HttpContent content, string saveTempPath)
        {
            // 检查是否是 multipart/form-data 
            if (!content.IsMimeMultipartContent("form-data"))
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            //文件保存目录路径 
            var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);

            //不存在目录则创建
            if (!Directory.Exists(dirTempPath))
            {
                if (!string.IsNullOrEmpty(dirTempPath))
                {
                    Directory.CreateDirectory(dirTempPath);
                }
            }

            // 设置上传目录 
            var provider = new MultipartFormDataStreamProvider(dirTempPath);
            var task = content.ReadAsMultipartAsync(provider).
                ContinueWith(o =>
                {
                    var hash = new Hashtable
                    {
                        ["Code"] = (int)ResponseCode.UploadFail,
                        ["Message"] = ResponseCode.UploadFail.GetDescription()
                    };
                    var file = provider.FileData[0];//provider.FormData 
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    var fileinfo = new FileInfo(file.LocalFileName);
                    //最大文件大小 
                    int maxSize = 10000000;
                    if (fileinfo.Length <= 0)
                    {
                        hash["Code"] = (int)ResponseCode.PleaseSelectFile;
                        hash["Message"] = ResponseCode.PleaseSelectFile.GetDescription();
                    }
                    else if (fileinfo.Length > maxSize)
                    {
                        hash["Code"] = (int)ResponseCode.UploadFileTooBig;
                        hash["Message"] = ResponseCode.UploadFileTooBig.GetDescription();
                    }
                    else
                    {
                        string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                        //定义允许上传的文件扩展名 
                        string fileTypes = "xlsx";
                        if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                        {
                            hash["Code"] = (int)ResponseCode.UnSupportFileType;
                            hash["Message"] = ResponseCode.UnSupportFileType.GetDescription();
                        }
                        else
                        {
                            var newFileName = DateTimeHelper.GetBeiJingNowDateTime().ToString("yyyyMMddHHmmss_ffff", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                            var uploadFileName = saveTempPath + newFileName + fileExt;
                            if (dirTempPath != null)
                            {
                                fileinfo.CopyTo(Path.Combine(dirTempPath, newFileName + fileExt), true);
                            }
                            fileinfo.Delete();
                            hash["Code"] = (int)ResponseCode.UploadSuccess;
                            hash["Message"] = ResponseCode.UploadSuccess.GetDescription();
                            hash["Data"] = uploadFileName;
                        }
                    }

                    //失败记录异常
                    if (o.IsFaulted && o.Exception != null)
                    {
                        ApiLogger.Error("HeadExcelUpload", "", $"异步上传Excel文件失败,错误{o.Exception.GetBaseException().Message}");
                    }

                    return hash;
                });
            return task;
        }

        /// <summary>
        /// 获取上传响应类
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static UploadFileResponse GetUploadFileResponse(string url)
        {
            return new UploadFileResponse
            {
                Url = url,
                FullUrl = FileHelper.GetFileFullUrl(url)
            };
        }
    }




    /// <summary>
    /// 
    /// </summary>
    public class UploadFileResponse
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullUrl { get; set; }
    }


}