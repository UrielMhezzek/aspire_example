using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TIK.Database.Context;

namespace TIK.Database.Services
{
    public static class DatabaseExtentions
    {
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.AddSqlServerDbContext<ApplicationDBContext>("sqldata");
        }



        public static void InitializeDatabase(this WebApplication app) {
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                    context.Database.EnsureCreated();
                }
            }
        }
    }

    //public class DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger) : BackgroundService
    //{


    //    public const string ActivitySourceName = "Migrations";

    //    private readonly ActivitySource _activitySource = new(ActivitySourceName);

    //    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    //    {
    //        using IServiceScope scope = serviceProvider.CreateScope();

    //        ApplicationDBContext context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

    //        await InitializeDatabaseAsync(context, cancellationToken);
    //    }

    //    private async Task InitializeDatabaseAsync(ApplicationDBContext context, CancellationToken cancellationToken)
    //    {
    //        using Activity? activity = _activitySource.StartActivity("Initializing Database", ActivityKind.Client);

    //        Stopwatch stopwatch = Stopwatch.StartNew();

    //        IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();

    //        await strategy.ExecuteAsync(context.Database.MigrateAsync, cancellationToken);

    //        await SeedAsync(context, cancellationToken);

    //        logger.LogInformation("Database Initialization Completed After {ElapsedMilliseconds}ms", stopwatch.ElapsedMilliseconds);
    //    }

    //    private async Task SeedAsync(ApplicationDBContext context, CancellationToken cancellationToken)
    //    {
    //        logger.LogInformation("Seeding Database");
    //    }
    //}
}
