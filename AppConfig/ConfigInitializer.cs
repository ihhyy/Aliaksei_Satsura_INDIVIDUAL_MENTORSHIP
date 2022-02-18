using System.Collections.Specialized;
using System.Configuration;

namespace AppConfig
{
    public static class ConfigInitializer
    {   
        public static NameValueCollection InitConfig(this NameValueCollection collection)
        {
            return ConfigurationManager.AppSettings;
        }
    }
}
