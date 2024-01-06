using TIK.Frontend.Server.Metrics;
using TIK.Database.Services;
using Microsoft.Extensions.Configuration;

namespace TIK.Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.AddServiceDefaults();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMetrics();
            builder.Services.AddSingleton<WeatherMetrics>();

            builder.AddAzureKeyVaultSecrets("secrets");

            //var connectionString = "Server=(localdb)\\mssqllocaldb;Database=test-Debug;Trusted_Connection=True;MultipleActiveResultSets=true;AttachDbFileName = D:\\OneDrive\\LocalDB\\Debug\\test-Debug.mdf;";

            builder.AddDatabase();

            //builder.Services.AddCors(options =>
            //{
            //    //options.AddPolicy("MyCorsPolicy", builder =>
            //    //{
            //    //    builder.WithOrigins("https://localhost:7151") // URL des Frontends
            //    //           .AllowAnyHeader()
            //    //           .AllowAnyMethod();
            //    //});
            //});

            var app = builder.Build();

            app.MapDefaultEndpoints();
            
            app.InitializeDatabase();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("MyCorsPolicy");
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
