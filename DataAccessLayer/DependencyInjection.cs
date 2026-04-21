using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration confiquraton)
        {
            //TO DO: Add Business Logic Layer services into the IoC container

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySQL(confiquraton.GetConnectionString("DefaultConnection")!);
            });

            return services;
        }
    }
}
