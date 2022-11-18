using Microsoft.AspNetCore.Mvc.Filters;

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
            Console.WriteLine($"请求 {executingContext.HttpContext.Request.Path.Value}");
        }

        /// <summary>
        /// 在 API 请求之后执行
        /// </summary>
        /// <param name="executedContext">操作完成后的上下文</param>
        public void OnActionExecuted(ActionExecutedContext executedContext)
        {
            if (executedContext.Canceled)
            {
                // 是否被其他 Filters 短路
            }
            else if (executedContext.Exception != null)
            {
                // 是否存在异常
            }
            else Console.WriteLine($"响应 {executedContext.Controller}");
        }

        /// <summary>
        /// 遇到错误时执行
        /// </summary>
        /// <param name="exceptionContext">错误上下文</param>
        public void OnException(ExceptionContext exceptionContext)
        {
            Console.WriteLine(exceptionContext.Exception.Message);
        }
    }
}
