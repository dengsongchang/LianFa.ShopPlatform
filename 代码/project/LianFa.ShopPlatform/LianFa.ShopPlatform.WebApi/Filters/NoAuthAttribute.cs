using System;

namespace LianFa.ShopPlatform.WebApi.Filters
{
    /// <summary>
    /// 指定在授权期间 System.Web.Http.AuthorizeAttribute 将进行不授权判断。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoAuthAttribute : Attribute
    {

    }
}