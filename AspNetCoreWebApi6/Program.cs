
using AspNetCoreWebApi6.Extentions;
using AspNetCoreWebApi6.Options;
using Serilog;
using Serilog.Events;

namespace AspNetCoreWebApi6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            ConfigureLogging(builder.Environment);

            if (Log.IsEnabled(Serilog.Events.LogEventLevel.Information))
                Log.Information("API Loging configured");

            builder.Configuration.AddJsonFile("appsettings.json");

            builder.Services.ConfigureAPI(builder.Configuration, builder.Environment, Log.Logger);
           
            var app = builder.Build();
            
            Log.Information("API build success");
           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Seed data directly here or call a method to do so
            SeedData.Initialize(app.Services);

            app.UseCors();  // Use CORS configured in ConfigureServices
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();  // Add this line to enable routing for controllers
            });

            app.Run();
        }

        
        
        /// <summary>
        /// method configure serilog Logging at startup 
        /// </summary>
        /// <param name="environment"></param>
        private static void ConfigureLogging(IHostEnvironment environment)
        {

            var minimumLogLevel = environment.IsDevelopment() ? LogEventLevel.Debug : LogEventLevel.Information;

            string logFilePath = Path.Combine(AppContext.BaseDirectory, "Logs\\log.txt");
            string logDirectory = Path.GetDirectoryName(logFilePath);

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            try
            {
                Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()  // Adjust the minimum log level here
                  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .Enrich.FromLogContext()
                 .WriteTo.Console()
                 .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
                 .CreateLogger();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configuring logger: {ex}");
            }

        }
    }
}