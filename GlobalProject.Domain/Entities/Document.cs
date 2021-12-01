using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalProject.Domain.Entities
{
    public interface IDocument
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        DateTime CreatedAt { get; set; }

        string Creator { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        DateTime LastUpdatedAt { get; set; }

        string LastUpdatedBy { get; set; }
    }
    public class Document : IDocument
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }

        public string Creator { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime LastUpdatedAt { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}
