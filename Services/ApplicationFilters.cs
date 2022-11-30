using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel;
using treeHolesApi.Model;

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
            Console.WriteLine($"接受请求:[{obj.Method}=>{obj.Path}]");

            //foreach(var item in obj.Headers)
            //{
            //    Console.WriteLine(item.Key, item.Value.ToString());
            //}
            Console.WriteLine(obj.Headers["User-Agent"].ToString());

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

        /// <summary>
        /// 遇到错误时执行
        /// </summary>
        /// <param name="exceptionContext">错误上下文</param>
        public void OnException(ExceptionContext exceptionContext)
        {
            Console.WriteLine(exceptionContext.Exception.Message);
        }


        /// <summary>
        /// 在操作结果之后执行
        /// </summary>
        /// <param name="resultExecuted">返回信息上下文</param>
        //public void OnResultExecuted(ResultExecutedContext resultExecuted)
        //{
        //    //ObjectResult result = (ObjectResult)resultExecuted.Result;

        //    Console.WriteLine($"{resultExecuted.HttpContext.Request.Method}=>{resultExecuted.HttpContext.Request.Path}");
        //    var obj = resultExecuted.HttpContext.Response.Body;

        //    Console.WriteLine($"[Action-AFTER] {obj}");
        //}

        ///// <summary>
        ///// 在操作结果之前执行
        ///// </summary>
        ///// <param name="resultExecuting">返回信息上下文</param>
        //public void OnResultExecuting(ResultExecutingContext resultExecuting)
        //{
        //    Console.WriteLine($"[Action-BEFORE] {resultExecuting.HttpContext.Response.Body}");
        //}
    }

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
