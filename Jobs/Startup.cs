using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using HangfireBasicAuthenticationFilter;
using MongoDB.Driver;

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

            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

            var mongoUrlBuilder = new MongoUrlBuilder(_configuration.GetValue<string>("ConnectionStrings:HangfireConnection"));

            var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

            services.AddHangfire(configuration => configuration
                 .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                 .UseSimpleAssemblyNameTypeSerializer()
                 .UseRecommendedSerializerSettings()
                 .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
                 {
                     MigrationOptions = new MongoMigrationOptions
                     {
                         MigrationStrategy = new MigrateMongoMigrationStrategy(),
                         BackupStrategy = new CollectionMongoBackupStrategy()
                     },
                     Prefix = "hangfire.mongo.dte",
                     CheckConnection = false,
                     CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection

                 })
             );

            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = "Hangfire.Mongo DT-e";
                serverOptions.Queues = new[] { "alpha", "beta", "default" };
            });


            services.AddScoped<TesteJob, TesteJob>();
        }
        public async Task Configure(WebApplication app, IWebHostEnvironment env, IBackgroundJobClient? backgroundJobs = null)
        {

            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                DashboardTitle = "DT-e Hanfire Dashboard",
                Authorization = new[]{
                new HangfireCustomBasicAuthenticationFilter{
                    User = _configuration.GetSection("HangfireCredentials:UserName").Value,
                    Pass = _configuration.GetSection("HangfireCredentials:Password").Value}}
            });

            app.MapHangfireDashboard();

            var recurringJobManager = app.Services.GetService<IRecurringJobManager>();
            var configuration = app.Services.GetService<IConfiguration>();
            //recurringJobManager.AddOrUpdate(, Hangfire.Common.Job.FromExpression<ITesteJob>(service => service.Executar()), );

            recurringJobManager.AddOrUpdate<TesteJob>(
               "IService.Heartbeat",
               job => job.Executar(),
               "0 1/1 * * * *",
               null
           );

            app.Run();
        }
    }
}