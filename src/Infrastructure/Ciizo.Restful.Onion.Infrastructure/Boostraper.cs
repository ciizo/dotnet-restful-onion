﻿using Ciizo.Restful.Onion.Domain.Core.Repository;
using Ciizo.Restful.Onion.Infrastructure.Persistence;
using Ciizo.Restful.Onion.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ciizo.Restful.Onion.Infrastructure
{
    public static class Boostraper
    {
        public static void RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CiizoDb"));
            }
            else
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
        }
    }
}