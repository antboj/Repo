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

        public static void ServiceRegisterOnCustomAttribute(this IServiceCollection service)
        {
            var allTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(e => e.GetCustomAttributes<ServiceAttribute>(true).Any());

            foreach (var type in allTypes)
            {
                var attrValue = type.GetCustomAttribute<ServiceAttribute>().Value;
                
                Type currentClass = type;
                Type[] allInterfaces = type.GetInterfaces();

                foreach (var currentInterface in allInterfaces)
                {
                    switch (attrValue)
                    {
                        case ServiceEnumAttributeValues.Scoped:
                            if (currentInterface.IsGenericType && currentClass.IsGenericType)
                            {
                                var gTi = currentInterface.GetGenericTypeDefinition();
                                service.AddScoped(gTi, currentClass);
                            }
                            else
                            {
                                service.AddScoped(currentInterface, currentClass);
                            }
                            break;
                        case ServiceEnumAttributeValues.Transient:
                            if (currentInterface.IsGenericType && currentClass.IsGenericType)
                            {
                                var gTi = currentInterface.GetGenericTypeDefinition();
                                service.AddScoped(gTi, currentClass);
                            }
                            else
                            {
                                service.AddTransient(currentInterface, currentClass);
                            }
                            break;
                        case ServiceEnumAttributeValues.Singleton:
                            if (currentInterface.IsGenericType && currentClass.IsGenericType)
                            {
                                var gTi = currentInterface.GetGenericTypeDefinition();
                                service.AddScoped(gTi, currentClass);
                            }
                            else
                            {
                                service.AddSingleton(currentInterface, currentClass);
                            }
                            break;
                    }
                }
            }
        }

        public static void FilterRegisterExtension(this IServiceCollection service)
        {
            var allFilters = Assembly.GetExecutingAssembly().GetTypes()
                .Where(e => e.GetCustomAttributes<FilterRegisterAttribute>(true).Any());

            service.AddMvc(options =>
            {
                foreach (var currentFilter in allFilters)
                {
                    options.Filters.Add(currentFilter);
                }
            });

        }
    }
}
