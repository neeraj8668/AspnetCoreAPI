using Microsoft.Extensions.Options;

namespace AspNetCoreWebApi6.Options
{
    public class CORSSettingsOptions : IConfigureOptions<CORSOptions>
    {
        public const string SectionName = "CORS";

        private readonly IConfiguration _configuration;

        public CORSSettingsOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(CORSOptions options)
        {
            _configuration
           .GetSection(SectionName)
           .Bind(options);
        }
    }

}
