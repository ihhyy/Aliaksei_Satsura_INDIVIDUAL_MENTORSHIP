using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Configurations
{
    public class TestConfig
    {
        public IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("testsettings.json")
                .Build();
            return config;
        }
    }
}
