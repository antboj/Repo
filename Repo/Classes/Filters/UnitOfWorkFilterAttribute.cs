using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Repo.Interfaces;

namespace Repo.Classes
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isStarted = false;
            var success = false;

            if (context.HttpContext.Request.Method != "GET")
            {
                _unitOfWork.Start();
                //success = true;
                isStarted = true;
            }
            //var success = false;
            try
            {
                await next();

                if (isStarted)
                {
                    _unitOfWork.Save();
                    success = true;
                }
            }
            catch (Exception)
            {
                success = false;
                throw;
            }
            finally
            {
                if (isStarted)
                {
                    if (success)
                    {
                        _unitOfWork.Commit();
                        _unitOfWork.Dispose();
                    }
                    else
                    {
                        _unitOfWork.Dispose();
                    }
                }
            }
        }
    }
}
