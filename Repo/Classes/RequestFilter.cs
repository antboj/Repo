using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Swashbuckle.AspNetCore.Swagger;

namespace Repo.Classes
{
    public class RequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            return;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //var isSet = context.ExceptionHandled;

            //var ex = new Error();
            //ex.Message = context.Exception.Message;
            //ex.Exception = context.Exception;
            //ex.StackTrace = context.Exception.StackTrace;

            //var sts = context.HttpContext.Response.StatusCode.Equals(400) ? true : false;

            if (context.HttpContext.Response.StatusCode.Equals(200))
            {

                var obj = new Response();
                //obj.Data = context.Result;
                obj.IsError = false;
                obj.Error = null;

                context.Result = new OkObjectResult(obj);
            }

            if (context.HttpContext.Response.StatusCode.Equals(400))
            {
                var err = new Error
                {
                    Message = "greska"
                };

                var obj = new Response();
                obj.Data = null;
                obj.IsError = true;
                obj.Error = err;

                context.Result = new BadRequestObjectResult(obj);
            }

            
            
        }
    }
}
