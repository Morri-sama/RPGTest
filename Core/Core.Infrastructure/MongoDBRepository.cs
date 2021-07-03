using Core.Domain;
using Core.Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Infrastructure
{
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

        public T GetById(long id)
        {
            return _mongoCollection.AsQueryable().Where(t => t.Id == id).FirstOrDefault();
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
