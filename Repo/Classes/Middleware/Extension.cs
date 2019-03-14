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

        //public static void AddAndrej(this IServiceCollection service)
        //{
        //    var types = from t in Assembly.GetExecutingAssembly().GetTypes()
        //                where t.GetCustomAttributes<ScopedServiceAttribute>().Any()
        //                select t;

        //    foreach (var t in types)
        //    {
        //        if (t.IsAbstract)
        //        {
        //            continue;
        //        }

        //        Type interF = t.GetInterfaces().FirstOrDefault();
        //        Type classN = t;

        //        // service.AddScoped(interF, classN);
        //        var method = typeof(ServiceCollectionServiceExtensions)
        //            .GetMethods(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x =>
        //                x.Name == "AddScoped" && x.IsGenericMethod && x.IsStatic && x.IsPublic &&
        //                x.GetGenericArguments().Length == 2);
        //        var genericMethod = method.MakeGenericMethod(new[] {interF, classN});
        //        genericMethod.Invoke(null, new[] {service});
        //    }
        //}

        public static void ServiceRegisterOnCustomAttribute(this IServiceCollection service)
        {
            //var types = from t in Assembly.GetExecutingAssembly().GetTypes()
            //    where t.GetCustomAttributes<ServiceAttribute>(true).Any()
            //    select t;

            var allTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(e => e.GetCustomAttributes<ServiceAttribute>(true).Any());

            foreach (var type in allTypes)
            {
                var attrValue = type.GetCustomAttribute<ServiceAttribute>().Value;

                if (type.IsAbstract)
                {
                    continue;
                }

                Type currentInterface = type.GetInterfaces().FirstOrDefault(x => !x.IsGenericType);
                Type currentClass = type;

                Type[] allInterfaces = type.GetInterfaces();

                foreach (var currInterface in allInterfaces)
                {
                    if (currInterface.IsGenericType)
                    {
                        Type[] genericParams = currInterface.GetGenericArguments();

                        

                        

                        service.AddScoped(currInterface, currentClass);
                    }
                }

                switch (attrValue)
                {
                    case ServiceEnumAttributeValues.Scoped:
                        service.AddScoped(currentInterface, currentClass);
                        break;
                    case ServiceEnumAttributeValues.Transient:
                        service.AddTransient(currentInterface, currentClass);
                        break;
                    case ServiceEnumAttributeValues.Singleton:
                        service.AddSingleton(currentInterface, currentClass);
                        break;
                }
            }
        }
    }
}
