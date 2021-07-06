using MongoDB.Driver;
using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RPGTest.Core.Infrastructure
{
    public class MongoDBRepository<T, TId> : IMongoDBRepository<T, TId> where T : EntityBase
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDBRepository(IMongoClient mongoClient, MongoDBSettings settings)
        {
            string typeName = settings.CollectionNames.GetValueOrDefault(typeof(T).Name);

            _mongoCollection = mongoClient.GetDatabase(settings.DatabaseName)
                                          .GetCollection<T>(typeName);
        }

        public void Delete(T entity)
        {
            _mongoCollection.FindOneAndDelete(filter => filter.Id == entity.Id);
        }

        public ICollection<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _mongoCollection.AsQueryable().Where(predicate.Compile()).ToList();
        }

        public ICollection<T> Get()
        {
            return _mongoCollection.AsQueryable().ToList();
        }


        public T GetById(TId id)
        {
            var filter = Builders<T>.Filter.Eq(t => t.Id, id.ToString());

            return _mongoCollection.Find(filter).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            _mongoCollection.InsertOne(entity);
        }

        public void Update(T entity)
        {
            _mongoCollection.FindOneAndReplace(filter => filter.Id == entity.Id, entity);
        }
    }

    public class MongoDBRepository<T> : IMongoDBRepository<T> where T : EntityBase
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoDBRepository(IMongoClient mongoClient, MongoDBSettings settings)
        {
            string typeName = settings.CollectionNames.GetValueOrDefault(typeof(T).Name);

            _mongoCollection = mongoClient.GetDatabase(settings.DatabaseName)
                                          .GetCollection<T>(typeName);
        }

        public void Delete(T entity)
        {
            _mongoCollection.FindOneAndDelete(filter => filter.Id == entity.Id);
        }

        public ICollection<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _mongoCollection.AsQueryable().Where(predicate.Compile()).ToList();
        }

        public ICollection<T> Get()
        {
            return _mongoCollection.AsQueryable().ToList();
        }

        public T GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq(t => t.Id, id.ToString());

            return _mongoCollection.Find(filter).FirstOrDefault();
        }

        public void Insert(T entity)
        {
            _mongoCollection.InsertOne(entity);
        }

        public void Update(T entity)
        {
            _mongoCollection.FindOneAndReplace(filter => filter.Id == entity.Id, entity);
        }
    }
}
