using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel;
using treeHolesApi.Model;
using System.Text.RegularExpressions;

namespace treeHolesApi.Services
{
    public class ApplicationFilters : IActionFilter, IExceptionFilter
    {
        /// <summary>
        /// 在 API 请求之前执行
        /// </summary>
        /// <param name="executingContext">操作开始前的上下文</param>
        public void OnActionExecuting(ActionExecutingContext executingContext)
        {
            HttpRequest obj = executingContext.HttpContext.Request;
            Console.WriteLine($"Request:[{obj.Method}=>{obj.Path}]");

            MediaCode mediaCode = UnitCheck(obj.Headers["User-Agent"]);

            var objs = new KeyValuePair<string, object>("media",mediaCode);
            executingContext.ActionArguments.Add(objs);
        }

        /// <summary>
        /// 在 API 请求之后执行
        /// </summary>
        /// <param name="executedContext">操作完成后的上下文</param>
        public void OnActionExecuted(ActionExecutedContext executedContext)
        {
            HttpRequest item = executedContext.HttpContext.Request;
            ObjectResult ?obj = executedContext.Result as ObjectResult;

            ComonFunc func = new(item.Path, obj);

            if (executedContext.Canceled)
            {
                // 是否被其他 Filters 短路
            }
            else if (executedContext.Exception != null)
            {
                // 是否存在异常
                Exception exception= executedContext.Exception;
                Console.WriteLine(exception.Message);
                executedContext.Exception = null;
                ResultInfo result = new(ResultInfoCode.ERROR, "无效操作", item.Path, null);
                executedContext.Result = new ObjectResult(result);
            }
            else if(obj.Value!=null)
            {
                if (obj.Value is List<TreeInfo>)
                {
                    executedContext.Result = func.ListObject();// 封装 返回结果
                }
                else if(obj.Value is Int32) {
                    executedContext.Result = func.IntObject();
                }
                
                Console.WriteLine($"Result:[{obj.Value}=>{item.Path}]");
            }
        }

        private readonly IHostEnvironment _hostEnvironment;

        public ApplicationFilters(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        /// <summary>
        /// 遇到错误时执行
        /// </summary>
        /// <param name="exceptionContext">错误上下文</param>
        public void OnException(ExceptionContext exceptionContext)
        {
            Console.WriteLine(exceptionContext.Exception.Message);
            if (!_hostEnvironment.IsDevelopment()) return;

            else {
                ResultInfo obj = new(ResultInfoCode.ERROR, "无效操作", exceptionContext.HttpContext.Request.Path, null);
                exceptionContext.Result = new ObjectResult(obj); }
        }

        /// <summary>
        /// 校验设备类型
        /// </summary>
        /// <param name="mediaUa">设备UA代码</param>
        /// <returns>返回识别后的设备代码（MediaCode）</returns>
        private static MediaCode UnitCheck(string mediaUa)
        {
            string regex = @"WeChat|Android|Windows";

            Regex regex1 = new(regex, RegexOptions.RightToLeft);

            Match li = regex1.Match(mediaUa);

            List<Group> sd = li.Groups.Values.ToList();

            return sd[0].Value switch
            {
                "Android" => MediaCode.Android,
                "Windows" => MediaCode.Pc,
                "WeChat" => MediaCode.Wx,
                _ => MediaCode.Other,
            };
        }
    }

    /// <summary>
    /// 设备代码
    /// </summary>
    public enum MediaCode
    {
        [Description("浏览器")]
        Web = 1,
        [Description("微信")]
        Wx,
        [Description("安卓")]
        Android,
        [Description("电脑")]
        Pc,
        [Description("其他")]
        Other
    }

    /// <summary>
    /// 信号代码
    /// </summary>
    public enum ResultInfoCode
    {
        [Description("有效操作")]
        SUCCESS = 1024,
        [Description("操作异常")]
        ERROR,
        [Description("无效操作")]
        FAIL,
        [Description("操作警告")]
        WARRING
    }

    /// <summary>
    /// 统一信息返回类
    /// </summary>
    public class ResultInfo
    {
        public ResultInfoCode Code { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? Url { get; set; }
        public Object? Data { get; set; }

        public ResultInfo(ResultInfoCode code, string message, string url, Object data)
        {
            Title = "Tip";
            Code = code;
            Message = message;
            Url = url;
            Data = data;
        }

    }


    /// <summary>
    /// 为过滤器服务的功能方法类，提供对List,Int的数据封装方法
    /// </summary>
    public class ComonFunc
    {
        public string path;
        public ObjectResult obj;

        public ComonFunc(string path, ObjectResult obj)
        {
            this.path = path;
            this.obj = obj;
        }

        /// <summary>
        /// List类型的封装方法
        /// </summary>
        /// <returns>ObjctResult类型的封装变量</returns>
        public ObjectResult ListObject() {
            List<TreeInfo>? info = obj.Value as List<TreeInfo>;

            ResultInfoCode code = info != null ? ResultInfoCode.SUCCESS : ResultInfoCode.WARRING;

            string ts = code == ResultInfoCode.WARRING ? "操作成功但存在缺陷" : "操作成功";

            ResultInfo os = new(code, ts, path, info);
            return new ObjectResult(os);
        }

        /// <summary>
        /// Int类型的封装方法
        /// </summary>
        /// <returns>ObjctResult类型的封装变量</returns>
        public ObjectResult IntObject()
        {
            int info = (int)obj.Value;

            ResultInfoCode code = info >= 1 ? ResultInfoCode.SUCCESS : ResultInfoCode.WARRING;

            string ts = code == ResultInfoCode.WARRING ? "操作成功但存在缺陷" : "操作成功";

            ResultInfo os = new(code, ts, path, info);
            return new ObjectResult(os);
        }
    }

}
