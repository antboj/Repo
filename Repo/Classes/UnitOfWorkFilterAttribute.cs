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
            _unitOfWork.Start();
            var success = false;
            try
            {
                await next();

                _unitOfWork.Save();
                success = true;
            }
            catch (Exception e)
            {
                success = false;
            }
            finally
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
