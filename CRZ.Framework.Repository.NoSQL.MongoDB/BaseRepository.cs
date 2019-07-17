using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace CRZ.Framework.Repository.NoSQL.MongoDB
{
    public abstract class BaseRepository<T>
        where T : class
    {
        readonly string _collection;

        readonly IMongoDatabase _database;

        protected IMongoCollection<T> Collection => _database.GetCollection<T>(_collection);

        protected BaseRepository(MongoDbOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            _collection = options.Collection;
            IMongoClient client = new MongoClient(options.ConnectionString);
            _database = client.GetDatabase(options.Database);
        }

        public long CountDocuments(Expression<Func<T, bool>> filter)
        {
            return Collection.CountDocuments(filter);
        }

        public long CountDocuments(FilterDefinition<T> filter)
        {
            return Collection.CountDocuments(filter);
        }

        public IEnumerable<T> GetDocuments()
        {
            return Collection.Find(_ => true).ToEnumerable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }

        public IEnumerable<T> Find(FilterDefinition<T> filter)
        {
            return Collection.Find(filter).ToEnumerable();
        }

        public T FindOne(Expression<Func<T, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public T FindOne(FilterDefinition<T> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public void Insert(IEnumerable<T> documents)
        {
            Collection.InsertMany(documents);
        }

        public void Insert(T document)
        {
            Collection.InsertOne(document);
        }

        public void Replace(Expression<Func<T, bool>> filter, T replacement)
        {
            Collection.ReplaceOne(filter, replacement);
        }

        public void Replace(FilterDefinition<T> filter, T replacement)
        {
            Collection.ReplaceOne(filter, replacement);
        }

        public void DeleteMany(Expression<Func<T, bool>> filter)
        {
            Collection.DeleteMany(filter);
        }

        public void DeleteMany(FilterDefinition<T> filter)
        {
            Collection.DeleteMany(filter);
        }

        public void Delete(Expression<Func<T, bool>> filter)
        {
            Collection.DeleteOne(filter);
        }

        public void Delete(FilterDefinition<T> filter)
        {
            Collection.DeleteOne(filter);
        }
    }
}
