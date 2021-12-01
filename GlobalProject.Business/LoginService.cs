using GlobalProject.Domain.Entities;
using GlobalProject.Domain.Model;
using GlobalProject.Infrastructure.AppEnums;
using GlobalProject.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GlobalProject.Business
{
    public interface ILoginService
    {
        Task<LoginLandingModel> GetLoginLandingViewDetails();
        Task Register(LoginRegisterModel request);
        Task Update(LoginUpdateModel request);
        Task Delete(string id);
    }

    public class LoginService : ILoginService
    {
        private readonly IMongoRepository<AuthenticationDocument> _authenticateRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IMongoRepository<AuthenticationDocument> authenticateRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _authenticateRepository = authenticateRepository;
            _httpContextAccessor = httpContextAccessor;
        }

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

        public async Task Register(LoginRegisterModel request)
        {
            if (!Enum.IsDefined(typeof(UserRoles), request.Role))
                throw new Exception("Role is not valid");

            var doc = new AuthenticationDocument()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Password = request.Password,
                Role = request.Role,
                Creator = _httpContextAccessor.HttpContext.User?.Identity?.Name,
                LastUpdatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name
            };

            await _authenticateRepository.InsertOneAsync(doc);
        }

        public async Task Update(LoginUpdateModel request)
        {
            var doc = await _authenticateRepository.FindOneAsync(filter => filter.UserName == request.Id);

            if (!Enum.IsDefined(typeof(UserRoles), request.Role))
                throw new Exception("Role is not valid");

            doc.Role = request.Role;
            doc.LastUpdatedBy = _httpContextAccessor.HttpContext.User?.Identity?.Name;
            doc.Password = request.Password;

            var filter = Builders<AuthenticationDocument>.Filter.Where(filter => filter.UserName == request.Id);
            await _authenticateRepository.ReplaceOneAsync(filter, doc);
        }

        public async Task Delete(string id)
        {
            await _authenticateRepository.DeleteByIdAsync(id);
        }
    }
}