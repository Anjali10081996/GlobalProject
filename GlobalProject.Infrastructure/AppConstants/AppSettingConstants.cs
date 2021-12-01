using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalProject.Infrastructure.AppConstants
{
    public class AppSettingConstants
    {
        public const string APIKeyProtectionEnabled = "AppSettings:APIKeyProtectionEnabled";
        public const string ConnectionString = "AppSettings:ConnectionString";
        public const string DatabaseName = "AppSettings:DatabaseName";
        public const string EncryptionKey = "AppSettings:EncryptionKey";
        public const string AuthApiKey = "AppSettings:AuthApiKey";
        public const string JaegerAgentHost = "AppSettings:JaegerAgentHost";
        public const string JaegerAgentPort = "AppSettings:JaegerAgentPort";
        public const string ServiceName = "AppSettings:ServiceName";
        public const string EnableLogInfo = "AppSettings:EnableLogInfo";
        public const string EnableLogWarning = "AppSettings:EnableLogWarning";
        public const string EnableLogError = "AppSettings:EnableLogError";
    }
}
