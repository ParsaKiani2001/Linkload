using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Application.Models;
using Infrastructure.Presistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Injection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddTransient<IResponceBase, ResponceBaseModel>();
            services.AddMemoryCache();
            services.AddScoped<IMainDbContext>(provider => provider.GetService<MainDbContext>()!);
            return services;
        }

    }
}
