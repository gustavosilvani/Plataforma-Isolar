using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using HangfireBasicAuthenticationFilter;
using Jobs.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Jobs
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração de Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
            });

            // Configuração do cliente MongoDB
            var mongoUrlBuilder = new MongoUrlBuilder(Configuration["ConnectionStrings:HangfireConnection"]);
            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            // Configuração do Hangfire usando MongoDB
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

            // Configuração do servidor Hangfire
            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Mongo";
                serverOptions.Queues = new[] { "alpha", "beta", "default" };
            });

            // Registro de serviços
            services.AddScoped<ITesteJob, TesteJob>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs = null)
        {
            // Configuração do Dashboard do Hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Hangfire Dashboard",
                Authorization = new[]
                {
                    new HangfireCustomBasicAuthenticationFilter
                    {
                        User = Configuration["HangfireCredentials:UserName"],
                        Pass = Configuration["HangfireCredentials:Password"]
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
                "0 1/1 * * * *"
            );

            app.Run();
        }
    }
}
