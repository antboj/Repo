using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repo.Classes
{
    public class ResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var o = context.Result as ObjectResult;
            if (o != null)
            {

                if (o.StatusCode >= StatusCodes.Status200OK && o.StatusCode < StatusCodes.Status300MultipleChoices)
                {
                    var obj = new Response();
                    obj.Data = o.Value;
                    obj.IsError = false;
                    obj.Error = null;

                    context.Result = new ObjectResult(obj);
                }

                //if (o.StatusCode >= StatusCodes.Status300MultipleChoices && o.StatusCode < StatusCodes.Status400BadRequest)
                //{
                //    var obj = new Response();
                //    obj.Data = o.Value;
                //    obj.IsError = false;
                //    obj.Error = null;

                //    context.Result = new ObjectResult(obj);
                //}

                if (o.StatusCode >= StatusCodes.Status400BadRequest && o.StatusCode < StatusCodes.Status500InternalServerError)
                {
                    var obj = new Response();
                    obj.Data = null;
                    obj.IsError = true;
                    obj.Error = new Error
                    {
                        Message = o.Value.ToString(),
                        Exception = null,
                        StackTrace = null
                    };

                    context.Result = new ObjectResult(obj) {StatusCode = o.StatusCode};
                }

                //if (o.StatusCode >= StatusCodes.Status500InternalServerError)
                //{
                //    var obj = new Response();
                //    obj.Data = null;
                //    obj.IsError = true;
                //    obj.Error.Message = "500";

                //    context.Result = new ObjectResult(obj);
                //}
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
