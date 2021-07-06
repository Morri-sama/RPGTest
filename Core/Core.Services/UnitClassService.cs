using Core.Domain;
using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class UnitClassService : IUnitClassService
    {
        private readonly IMongoDBRepository<UnitClass> _repository;

        public UnitClassService(IMongoDBRepository<UnitClass> repository)
        {
            _repository = repository;
        }

        public void Delete(UnitClass unitClass)
        {
            _repository.Delete(unitClass);
        }

        public ICollection<UnitClass> GetAll()
        {
            return _repository.Get();
        }

        public void Insert(UnitClass unitClass)
        {
            _repository.Insert(unitClass);
        }

        public void Update(UnitClass unitClass)
        {
            _repository.Update(unitClass);
        }
    }
}
