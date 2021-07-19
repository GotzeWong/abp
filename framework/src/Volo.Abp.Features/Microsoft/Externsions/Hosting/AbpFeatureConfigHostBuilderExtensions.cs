using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Hosting
{
    public static class AbpFeatureConfigHostBuilderExtensions
    {
        public static IHostBuilder UseFeatureConfig(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("featuredefinitions.json", optional: true, reloadOnChange: true);
            });
        }
    }
}
