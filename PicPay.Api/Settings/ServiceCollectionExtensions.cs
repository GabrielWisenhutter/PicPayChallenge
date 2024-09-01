using Microsoft.Extensions.DependencyInjection;
using PicPay.Domain.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Api.Settings
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHandler(this IServiceCollection services, Assembly assembly)
        {
            var handlerTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>))).ToList();


            foreach (var handlerType in handlerTypes)
            {
                // Encontra a interface implementada pelo handler
                var handlerInterface = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<>));

                services.AddScoped(handlerInterface, handlerType);
            }
        }
    }
}
