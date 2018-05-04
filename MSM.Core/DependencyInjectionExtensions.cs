using System;
using System.Collections.Generic;
using System.Text;
using MSM.Core.Authentication;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMinecraftServerManager(this IServiceCollection services, bool isTest)
        {
            if (!isTest) {
                services.AddTransient<IMojangAuthenticator, MojangAuthenticator>();
            } else {
                services.AddTransient<IMojangAuthenticator, TestAuthenticator>();
            }

            return services;
        }
    }
}
