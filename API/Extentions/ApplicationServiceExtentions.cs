using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extentions
{
    //because I am going to be writing extention methods, I need to make this class static
    //becaseu it is a static class, I can use the methods inside it WITHOUT instantiating a new instance of this class, I can just use it
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors();
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<ITokenService, TokenService>();
            //^the advantage of including the interface here in addition tot he actual service I want to inject is.....
            //when i am testing my code, it is much easier to test against interfaces and isolate code
            //using an interface I can mock the implementation to test only the code I need to

            return services;
        }
    }
}