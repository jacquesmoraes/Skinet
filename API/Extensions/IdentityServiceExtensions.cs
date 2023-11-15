using Core.Entities.Identity;
using Infrastructure.Idedntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityDbContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
            });
            services.AddIdentityCore<AppUser>(options =>
            {// can add options here
            }).AddEntityFrameworkStores<AppIdentityDbContext>().
            AddSignInManager<SignInManager<AppUser>>();

            services.AddAuthentication();
            services.AddAuthorization();

            return services;
        }
    }
}
