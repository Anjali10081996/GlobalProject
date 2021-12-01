using GlobalProject.Infrastructure.AppConstants;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalProject.Business.ConfigSettings
{
    public class AppSettings
    {
        public static AppSettings Current;

        public AppSettings()
        {
            Current = this;
        }

        public string APIKeyProtectionEnabled { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string EncryptionKey { get; set; }
        public string AuthApiKey { get; set; }
        public string JaegerAgentHost { get; set; }
        public string JaegerAgentPort { get; set; }
        public string ServiceName { get; set; }
        public string EnableLogInfo { get; set; }
        public string EnableLogWarning { get; set; }
        public string EnableLogError { get; set; }

        public static IEnumerable<KeyValuePair<string, string>> GetKeyValueCollection(AppSettings cupsSettings)
        {
            return new[]
            {
                new KeyValuePair<string, string>(AppSettingConstants.APIKeyProtectionEnabled, cupsSettings.APIKeyProtectionEnabled),
                new KeyValuePair<string, string>(AppSettingConstants.ConnectionString, cupsSettings.ConnectionString),
                new KeyValuePair<string, string>(AppSettingConstants.DatabaseName, cupsSettings.DatabaseName),
                new KeyValuePair<string, string>(AppSettingConstants.EncryptionKey, cupsSettings.EncryptionKey),
                new KeyValuePair<string, string>(AppSettingConstants.AuthApiKey, cupsSettings.AuthApiKey),
                new KeyValuePair<string, string>(AppSettingConstants.JaegerAgentHost, cupsSettings.JaegerAgentHost),
                new KeyValuePair<string, string>(AppSettingConstants.JaegerAgentPort, cupsSettings.JaegerAgentPort),
                new KeyValuePair<string, string>(AppSettingConstants.ServiceName, cupsSettings.ServiceName),
                new KeyValuePair<string, string>(AppSettingConstants.EnableLogInfo, cupsSettings.EnableLogInfo),
                new KeyValuePair<string, string>(AppSettingConstants.EnableLogWarning, cupsSettings.EnableLogWarning),
                new KeyValuePair<string, string>(AppSettingConstants.EnableLogError, cupsSettings.EnableLogError)
            };
        }
    }
}
