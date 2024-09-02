using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PicPay.Domain.Extensions;
using PicPay.Domain.Handlers;
using PicPay.Domain.Interfaces;
using PicPay.Infra.Data.Context;
using PicPay.Infra.Data.Repositories;
using PicPay.Infra.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicPay.Infra.Data
{
    public static class Container
    {
        public static IServiceCollection AddPicPay(this IServiceCollection services, IConfiguration configuration)
        {
            AddDb(services, configuration);
            AddRepositories(services);
            AddServices(services);
            AddHandlers(services);

            return services;
        }

        private static IServiceCollection AddDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PicPayContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        private static IServiceCollection AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();

            return services;
        }

        private static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<Notification>();

            return services;
        }

        private static IServiceCollection AddHandlers(IServiceCollection services)
        {
            services.AddScoped<WalletHandler, WalletHandler>();
            services.AddScoped<TransferHandler, TransferHandler>();

            return services;
        }
    }
}
