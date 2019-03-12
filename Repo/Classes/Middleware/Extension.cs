using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Repo.Classes.Attributes;
using Repo.Interfaces;

namespace Repo.Classes
{
    public static class Extension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void AddAndrej(this IServiceCollection service)
        {
            var types = from t in Assembly.GetExecutingAssembly().GetTypes()
                        where t.GetCustomAttributes<ScopedServiceAttribute>().Any()
                        select t;
            
            foreach (var t in types)
            {
                if (t.IsAbstract)
                {
                    continue;
                }

                Type interF = t.GetInterfaces().FirstOrDefault();
                Type classN = t;

                // service.AddScoped(interF, classN);
                var method = typeof(ServiceCollectionServiceExtensions)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x =>
                        x.Name == "AddScoped" && x.IsGenericMethod && x.IsStatic && x.IsPublic &&
                        x.GetGenericArguments().Length == 2);
                var genericMethod = method.MakeGenericMethod(new[] {interF, classN});
                genericMethod.Invoke(null, new[] {service});
            }
        }

    }
}
