using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using HangfireBasicAuthenticationFilter;
using Jobs.Interfaces;
using MongoDB.Driver;
using Infra.Ioc;

namespace Jobs
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

            services.AddContext(_configuration);
            services.AddServices();
            services.AddRepositorys();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
            });

            var mongoUrlBuilder = new MongoUrlBuilder(_configuration["ConnectionStrings:HangfireConnection"]);
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
                      {
                          MigrationOptions = new MongoMigrationOptions
                          {
                              MigrationStrategy = new MigrateMongoMigrationStrategy(),
                              BackupStrategy = new CollectionMongoBackupStrategy()
                          },
                          Prefix = "hangfire.mongo",
                          CheckConnection = false,
                          CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection
                      });
            });

            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Mongo";
                serverOptions.Queues = new[] { "alpha", "beta", "default" };
            });

            services.AddScoped<ITesteJob, TesteJob>();
        }

        public async Task Configure(WebApplication app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs = null)
        {
            // Configuração do Dashboard do Hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Hangfire Dashboard",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = _configuration["HangfireCredentials:UserName"],
                        Pass = _configuration["HangfireCredentials:Password"]
                    }
                }
            });

            app.MapHangfireDashboard();
            app.UseHangfireServer();

            var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();

            // Configuração de um trabalho recorrente
            recurringJobManager.AddOrUpdate<ITesteJob>(
                "IService.Heartbeat",
                job => job.Executar(),
                "0 0/60 * * * *"
            );

            app.Run();
        }
    }
}
