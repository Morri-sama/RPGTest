﻿using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTest.Core.Services
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

        public UnitClass GetById(string id)
        {
            var unitClass = _repository.GetById(id);

            return unitClass;
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