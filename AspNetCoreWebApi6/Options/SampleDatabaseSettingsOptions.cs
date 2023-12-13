using Microsoft.Extensions.Options;

namespace AspNetCoreWebApi6.Options
{
    public class SampleDatabaseSettingsOptions : IConfigureOptions<SampleDatabaseSettings>
    {
        private const string SectionName = "SmpleContextDb";

        private readonly IConfiguration _configuration;

        public SampleDatabaseSettingsOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(SampleDatabaseSettings options)
        {
            _configuration
           .GetSection(SectionName)
           .Bind(options);
        }
    }

}
