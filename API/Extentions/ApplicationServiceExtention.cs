using API.Context;
using API.Interface;
using API.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServiceExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config )
        {
            services.AddDbContext<DataContext>(options=>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<ITokenService,TokenService>();
            return services;
        }
    }
}