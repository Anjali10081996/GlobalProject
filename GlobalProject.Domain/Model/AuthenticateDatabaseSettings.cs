using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalProject.Domain.Model
{
    public interface IAuthenticateDatabaseSettings
    {
        public string AuthenticateCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    
    public class AuthenticateDatabaseSettings : IAuthenticateDatabaseSettings
    {
        public string AuthenticateCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
