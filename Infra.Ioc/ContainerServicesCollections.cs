using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infra.Data.Context;
using Microsoft.AspNetCore.Http;

namespace Infra.Ioc
{
    public static class ContainerServicesCollections
    {
        public static IServiceCollection ConfigureApi(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CaixaPostal.Api", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });


            return services;
        }

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            MongoDbContext.ConnectionString = configuration.GetSection("MongoConnection:ConnectionString").Value?? string.Empty;
            MongoDbContext.DatabaseName = configuration.GetSection("MongoConnection:Database").Value ?? string.Empty;
            MongoDbContext.IsSSL = Convert.ToBoolean(configuration.GetSection("MongoConnection:IsSSL").Value);

            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            return services;
        }


        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            //services.AddScoped<IUsuario, Usuario>();
            //services.AddScoped<IAuthService, AuthService>();
            


            return services;
        }

    }
}
