using GlobalProject.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GlobalProject.Repository.Repositories
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        Task InsertOneAsync(TDocument document);
        Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TDocument> filter, TDocument document, bool isUpsert = true);
        Task DeleteByIdAsync(string id);
        Task<List<TDocument>> FindAll(FilterDefinition<TDocument> filter, ProjectionDefinition<TDocument> projection);
    }
}
