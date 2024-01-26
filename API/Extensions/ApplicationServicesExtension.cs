using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services, IConfiguration config)
        {
         

            Services.AddControllers();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("cannot create 'DefaultConnection' on database")));
            
            Services.AddSingleton<IConnectionMultiplexer>(x => {
                var options =ConfigurationOptions.Parse(config.
                GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            Services.AddScoped<IBasketRepository, BasketRepository>();
            
            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Services.Configure<ApiBehaviorOptions>(options =>
                options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState.
                Where(x => x.Value.Errors.Count > 0).
                SelectMany(x => x.Value.Errors).
                Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };
                return new BadRequestObjectResult(errorResponse);
            });
            Services.AddCors(opt =>
            {
                opt.AddPolicy(
                    name: "AllowOrigin", 
                    builder  => {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            return Services;
        }
    }
}
