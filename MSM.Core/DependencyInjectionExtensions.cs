﻿using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using MSM.Core.Authentication;
using MSM.Core.Config;
using MSM.Core.GameData;

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

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddSingleton<LiteDatabase>(new LiteDatabase("filename=msm.db;upgrade=true"));
            services.AddTransient<IMetadataProvider, MetadataProvider>();
            return services;
        }
    }
}
