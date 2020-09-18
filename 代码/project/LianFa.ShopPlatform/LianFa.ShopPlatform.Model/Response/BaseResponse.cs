namespace LianFa.ShopPlatform.Model.Response
{
    /// <summary>
    /// 泛型响应类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// 响应码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 响应消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 响应数据
        /// </summary>
        public T Data { get; set; }
    }
}
