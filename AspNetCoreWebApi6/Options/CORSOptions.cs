using Microsoft.Extensions.Options;

namespace AspNetCoreWebApi6.Options
{
    public class CORSOptions
    {
        public string[] AllowedHosts { get; set; }

    }
}
