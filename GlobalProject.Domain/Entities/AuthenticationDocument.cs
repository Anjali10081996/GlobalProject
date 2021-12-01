using GlobalProject.Infrastructure.Attibutes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalProject.Domain.Entities
{
    [BsonCollection("Authentication")]
    public class AuthenticationDocument : Document
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
