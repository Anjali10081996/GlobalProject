using GlobalProject.Infrastructure.AppEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GlobalProject.Domain.Model
{
    public class LoginLandingModel
    {
        public string CurrentUserName { get; set; }
        public string Message { get; set; }
        public IEnumerable<UserView> Users { get; set; }
        public List<string> Roles { get; } = Enum.GetValues(typeof(UserRoles))
                                            .Cast<UserRoles>()
                                            .Select(s => s.ToString())
                                            .ToList();
    }

    public class UserView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
