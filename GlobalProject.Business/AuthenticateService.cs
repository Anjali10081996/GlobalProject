using GlobalProject.Domain.Entities;
using GlobalProject.Domain.Model;
using GlobalProject.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalProject.Business
{
 
    public class AuthenticateService
    {
        private readonly IMongoCollection<UserModel> _user;
        private readonly IMongoRepository<AuthenticationDocument> _authenticateRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthenticateService(IAuthenticateDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _user = database.GetCollection<UserModel>(settings.AuthenticateCollectionName);
        }

        public List<UserModel> Get()
        {
            List<UserModel> user;
            user = _user.Find(emp => true).ToList();
            return user;
        }

        public UserModel Get(string id) =>
            _user.Find<UserModel>(emp => emp.Id == id).FirstOrDefault();
        public async Task<LoginLandingModel> GetLoginLandingViewDetails()
        {
            var filter = Builders<AuthenticationDocument>.Filter.Empty;
            var projection = Builders<AuthenticationDocument>.Projection.Include("Id").Include("UserName").Include("Role");
            var content = await _authenticateRepository.FindAll(filter, projection);

            var users = new List<UserView>();
            content.ForEach(x => users.Add(new UserView { Id = x.Id, UserName = x.UserName, Role = x.Role }));

            var model = new LoginLandingModel()
            {
                CurrentUserName = _httpContextAccessor.HttpContext.User?.Identity?.Name,
                Users = users
            };

            return model;
        }
    }
}
