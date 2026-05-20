using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Application.Core.Configuration.Environment
{
    public class EnvironmentService : IEnvironmentService
    {
        private static IConfiguration _configuration;

        private static string _environmentVariableValue = string.Empty;
        private static string _environmentVariableKey = "ENVIRONMENT";

        public EnvironmentService()
        {
            SetEnvironmentValue();
            SetConfiguration();
        }

        public IConfiguration Configuration => _configuration;

        private void SetConfiguration()
        {
            if (_configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                var appsettingsFileName = $"appsettings.development.json";

                _configuration = configurationBuilder.SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile(appsettingsFileName, optional: false, reloadOnChange: true)
                    .Build();
            }
        }

        private void SetEnvironmentValue()
        {
            if (Debugger.IsAttached)
            {
                _environmentVariableValue = System.Environment.GetEnvironmentVariable(_environmentVariableKey);
            }
            else
            {
                _environmentVariableValue = System.Environment.GetEnvironmentVariable(_environmentVariableKey, EnvironmentVariableTarget.Process);
            }
        }

        public bool IsDevelopment => _environmentVariableValue.ToLower() == "development";
        public bool IsStaging => _environmentVariableValue.ToLower() == "staging";
        public bool IsTest => _environmentVariableValue.ToLower() == "test";
        public bool IsProduction => _environmentVariableValue.ToLower() == "production";
    }
}
