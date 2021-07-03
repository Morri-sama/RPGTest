using Core.Domain;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class ClassService : IClassService
    {
        private readonly IMongoDBRepository<Class> _repository;

        public ClassService(IMongoDBRepository<Class> repository)
        {
            _repository = repository;
        }

        public void Delete(Class entity)
        {
            _repository.Delete(entity);
        }

        public ICollection<Class> GetAll()
        {
            return _repository.Get();
        }

        public void Insert(Class entity)
        {
            _repository.Insert(entity);
        }

        public void Update(Class entity)
        {
            _repository.Update(entity);
        }
    }
}
