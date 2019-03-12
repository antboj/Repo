using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repo.Classes.Attributes;

namespace Repo.Classes
{
    public class RegisterServices<TI, T> where TI : class where T : class, TI

    {
        private readonly IServiceCollection _service;

        public RegisterServices(IServiceCollection service)
        {
            _service = service;
        }

        public void AddScopedService()
        {
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                where t.GetCustomAttributes<ScopedServiceAttribute>().Any()
                select t;

            foreach (var t in types)
            {
                _service.AddScoped<TI, T>();
            }
        }
    }
}
