using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repo.Classes.Attributes;

namespace Repo.Classes
{
    [FilterRegister]
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var o = context.Result as ObjectResult;
            
            var err = new Error
            {
                Message = context.Exception.Message,
                Exception = context.Exception.Data.ToString(),
                StackTrace = context.Exception.StackTrace
            };

            if (context.Exception.GetBaseException() is SqlException ex)
            {
                var num = ex.Number;
                if (num == 547)
                {
                    err.Message = "No";
                }
            }

            var obj = new Response {Data = null, IsError = true, Error = err};
            context.Result = new ObjectResult(obj) { StatusCode = StatusCodes.Status500InternalServerError };
        }
    }
}
