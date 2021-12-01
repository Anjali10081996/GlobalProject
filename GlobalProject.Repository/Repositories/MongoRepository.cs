using GlobalProject.Domain.Entities;
using GlobalProject.Domain.Model;
using GlobalProject.Infrastructure.Attibutes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GlobalProject.Repository.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        public MongoRepository(IAuthenticateDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TDocument>(settings.AuthenticateCollectionName);

        }

        public List<TDocument> Get()
        {
            List<TDocument> authenticate;
            authenticate = _collection.Find(auth => true).ToList();
            return authenticate;
        }

        public TDocument Get(string id) =>
            _collection.Find<TDocument>(auth => auth.Id == id).FirstOrDefault();

    
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return await _collection.Find(filterExpression).SingleOrDefaultAsync();
        }
        public virtual async Task<List<TDocument>> FindAll(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument> projection)
        {
            return await Task.FromResult(_collection.Find(filter).Project<TDocument>(projection).ToList());
        }
        public virtual async Task InsertOneAsync(TDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            document.CreatedAt = DateTime.UtcNow;
            document.LastUpdatedAt = DateTime.UtcNow;

            await _collection.InsertOneAsync(document);
        }
        public async Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TDocument> filter, TDocument document, bool isUpsert = true)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            document.LastUpdatedAt = DateTime.UtcNow;
            var replaceOptions = new ReplaceOptions()
            {
                IsUpsert = isUpsert
            };

            return await _collection.ReplaceOneAsync(filter, document, replaceOptions);
        }
        public virtual async Task DeleteByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.FindOneAndDeleteAsync(filter);
        }

    }
}
