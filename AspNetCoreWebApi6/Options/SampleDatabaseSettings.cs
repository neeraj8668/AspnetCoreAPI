using Microsoft.Extensions.Options;

namespace AspNetCoreWebApi6.Options
{
    public class SampleDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollectionName { get; set; } = null!;
        public string ProductCollectionName { get; set; } = null!;

    }
}
