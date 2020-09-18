using System;
using System.Collections.Generic;
using System.Globalization;
using HuCheng.Util.Oss.AliYun.Response;
using MetadataExtractor;


namespace LianFa.ShopPlatform.Code.Image
{
    public class ImageExifHelper
    {
        /// <summary>
        /// 转换Oss图片Exif信息为格式化的Exif信息
        /// </summary>
        /// <param name="imgExifInfo">Oss图片Exif信息</param>
        /// <returns></returns>
        public static ExifModel ConvertOssImageExif(ImageInfoResponse imgExifInfo)
        {
            //获取型号信息
            var device = imgExifInfo.Make == null ? "" : imgExifInfo.Make.value;
            if (imgExifInfo.Model != null)
            {
                //如型号不包含制造商则拼接,包含则为型号信息
                if (imgExifInfo.Model.value.Contains(device))
                {
                    device = imgExifInfo.Model.value;
                }
                else
                {
                    device += " " + imgExifInfo.Model.value;
                }
            }

            //获取焦距信息
            var foci = imgExifInfo.FocalLength == null ? "" : GetExifSpecificValue(imgExifInfo.FocalLength.value);
            if (!string.IsNullOrEmpty(foci))
            {
                foci += " mm";

                //35mm焦距
                var mmFoci = imgExifInfo.FocalLengthIn35mmFilm == null ? "" : imgExifInfo.FocalLengthIn35mmFilm.value;
                if (!string.IsNullOrEmpty(mmFoci))
                {
                    foci += " (35mm equivalent:" + mmFoci + " mm)";
                }
            }

            //获取图片拍摄时间
            string shootingTime;
            if (imgExifInfo.DateTimeOriginal == null)
            {
                if (imgExifInfo.DateTimeDigitized == null)
                {
                    shootingTime = imgExifInfo.DateTime == null ? "" : imgExifInfo.DateTime.value;
                }
                else
                {
                    shootingTime = imgExifInfo.DateTimeDigitized == null ? "" : imgExifInfo.DateTimeDigitized.value;
                }
            }
            else
            {
                shootingTime = imgExifInfo.DateTimeOriginal.value;
            }

            //格式化Exif信息
            var exifInfo = new ExifModel
            {
                Aperture = imgExifInfo.FNumber == null ? "" : GetExifSpecificValue(imgExifInfo.FNumber.value, 1),
                Camera = imgExifInfo.LensModel == null ? "" : imgExifInfo.LensModel.value,
                Device = device,
                ExposureCompensation = imgExifInfo.ExposureBiasValue == null ? "" : GetExifSpecificValue(imgExifInfo.ExposureBiasValue.value, 3),
                Foci = foci,
                ISO = imgExifInfo.ISOSpeedRatings == null ? "" : imgExifInfo.ISOSpeedRatings.value,
                Shutter = imgExifInfo.ExposureTime == null ? "" : imgExifInfo.ExposureTime.value + "秒",
                ShootingTime = shootingTime
            };

            return exifInfo;
        }

        /// <summary>
        /// 获取Exif具体数值
        /// </summary>
        /// <param name="exifInfo">exif数值</param>
        /// <param name="digits">小数点后保留几位,默认2位</param>
        /// <returns></returns>
        private static string GetExifSpecificValue(string exifInfo, int digits = 2)
        {
            try
            {
                //通过分隔符/获取数值
                var values = exifInfo.Split('/');
                if (values.Length < 2)
                {
                    return "";
                }

                //获取除数
                var divisor = Convert.ToDouble(values[0]);

                //获取被除数
                var dividend = Convert.ToDouble(values[1]);

                //被除数等于0直接返回空字符
                if (Convert.ToInt32(dividend) == 0)
                {
                    return "";
                }

                //获取四舍五入的计算结果
                var result = Math.Round(Convert.ToDouble(divisor / dividend), digits, MidpointRounding.AwayFromZero);
                return result.ToString(CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 获取图片Exif信息
        /// </summary>
        /// <param name="imgPath"></param>
        /// <returns></returns>
        public static ExifModel GetImageExif(string imgPath)
        {
            //读取图片的信息
            var rmd = ImageMetadataReader.ReadMetadata(imgPath);

            //转为Dictionary
            var dict = GetExifDict(rmd);

            //返回模型
            return GetExifModel(dict);
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        private static ExifModel GetExifModel(Dictionary<string, string> rt)
        {
            var data = new ExifModel
            {
                Aperture = "",
                Camera = "",
                Device = "",
                ExposureCompensation = "",
                Foci = "",
                ISO = "",
                Shutter = "",
                ShootingTime = ""
            };
            //设备类型
            if (rt.ContainsKey("Make"))
                data.Device = rt["Make"];
            if (rt.ContainsKey("Model"))
                data.Device += rt["Model"];
            //镜头
            if (rt.ContainsKey("Lens Model"))
                data.Camera = rt["Lens Model"];
            //时间
            if (rt.ContainsKey("Date/Time Original"))
                data.ShootingTime = rt["Date/Time Original"];
            //快门
            if (rt.ContainsKey("Shutter Speed Value"))
                data.Shutter = rt["Shutter Speed Value"];
            //光圈
            if (rt.ContainsKey("F-Number"))
                data.Aperture = rt["F-Number"];
            //ISO
            if (rt.ContainsKey("ISO Speed Ratings"))
                data.ISO = rt["ISO Speed Ratings"];
            //曝光补偿
            if (rt.ContainsKey("Exposure Bias Value"))
                data.ExposureCompensation = rt["Exposure Bias Value"];
            //焦距
            if (rt.ContainsKey("Focal Length"))
                data.Foci = rt["Focal Length"];

            return data;
        }

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetExifDict(IReadOnlyList<Directory> rmd)
        {
            var rt = new Dictionary<string, string>();
            foreach (var item in rmd)
            {
                foreach (var tagItem in item.Tags)
                {
                    if (!rt.ContainsKey(tagItem.Name))
                    {
                        rt.Add(tagItem.Name, tagItem.Description);
                    }
                }
            }
            return rt;
        }
    }



    public class ExifModel
    {
        /// <summary>  
        /// 设备型号  
        /// </summary>
        public string Device { get; set; }
        /// <summary>  
        /// 焦距  
        /// </summary>
        public string Foci { get; set; }
        /// <summary>  
        /// 光圈  
        /// </summary>
        public string Aperture { get; set; }
        /// <summary>  
        /// 快门  
        /// </summary>
        public string Shutter { get; set; }
        /// <summary>  
        /// ISO  
        /// </summary>
        public string ISO { get; set; }
        /// <summary>  
        /// 曝光补偿  
        /// </summary>
        public string ExposureCompensation { get; set; }
        /// <summary>  
        /// 镜头  
        /// </summary>
        public string Camera { get; set; }
        /// <summary>  
        ///   
        /// </summary>
        public string ShootingTime { get; set; }
    }
}
