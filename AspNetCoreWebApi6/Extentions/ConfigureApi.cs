using AspNetCoreWebApi6.Options;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace AspNetCoreWebApi6.Extentions
{
    public static class ConfigureApi
    {
        /// <summary>
        /// method to configure Services used in the application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="_logger"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureAPI(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env, Serilog.ILogger _logger)
        {
            // configuring Database setting options 
            services.ConfigureOptions<SampleDatabaseSettingsOptions>();

            // configuring CORS options
            services.ConfigureOptions<CORSSettingsOptions>();

            var corsOptions = new CORSOptions();
            configuration.GetSection(CORSSettingsOptions.SectionName).Bind(corsOptions);

            //setting up orgins for cofigured client urls 
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsOptions.AllowedHosts)
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            // adding Mongoclient service 
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<SampleDatabaseSettings>>().Value;
                return new MongoClient(settings.ConnectionString);
            });

            //adding Users and product services
            services.AddSingleton<IUserSevice, UserService>();
            services.AddSingleton<IProductSevice, ProductService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //adding controllers
            services.AddControllers(options =>
            {
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
                options.Filters.Add<ApiExceptionFilterAttribute>(); 
                options.Filters.Add<ValidateModelAttribute>();  
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null); 
           
               

            services.AddEndpointsApiExplorer();

            // adding swagger support 
            services.AddSwaggerGen();

            return services;
        }
    }
}
