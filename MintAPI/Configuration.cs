using System;
using Microsoft.Extensions.Configuration;

namespace MintAPI
{
    public interface IConfigurationSetting
    {
        public string GetEnvironmentVariable(string name);
    }

    public class ConfigurationSetting : IConfigurationSetting
    {
        private readonly IConfiguration _configuration;

        public ConfigurationSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetEnvironmentVariable(string name)
        {
            try
            {
                string setting = System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);

                if(string.IsNullOrEmpty(setting))
                {
                    setting = _configuration[name];
                }

                return setting;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
