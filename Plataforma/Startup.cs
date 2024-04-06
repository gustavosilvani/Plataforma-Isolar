using Infra.Ioc;
using MongoDB.Driver;

namespace Plataforma
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.ConfigureApi(_configuration);
            services.AddEndpointsApiExplorer();
            services.AddSwagger(_configuration);
            services.AddContext(_configuration);
            services.AddServices();
            //services.AddScoped<ResultadoCustomizadoFiltro>();

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

            var mongoUrlBuilder = new MongoUrlBuilder(_configuration.GetValue<string>("ConnectionStrings:HangfireConnection"));

            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());
        }
        public async Task Configure(WebApplication app, IWebHostEnvironment env)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}
            app.UseHsts();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}