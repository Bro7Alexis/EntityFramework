using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace MyProject
{
    public static class SwaggerConfig
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API EF", Version = "v1" });
            });
        }
    }
}