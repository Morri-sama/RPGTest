using RPGTest.Core.Domain;
using RPGTest.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTest.Core.Services
{
    public class UnitClassService : IUnitClassService
    {
        private readonly IMongoDBRepository<UnitClass> _unitClassRepository;

        public UnitClassService(IMongoDBRepository<UnitClass> unitClassRepository)
        {
            _unitClassRepository = unitClassRepository;
        }

        public void Delete(UnitClass unitClass)
        {
            _unitClassRepository.Delete(unitClass);
        }

        public ICollection<UnitClass> GetAll()
        {
            return _unitClassRepository.Get();
        }

        public UnitClass GetById(string id)
        {
            var unitClass = _unitClassRepository.GetById(id);

            return unitClass;
        }

        public void Insert(UnitClass unitClass)
        {
            _unitClassRepository.Insert(unitClass);
        }

        public void Update(UnitClass unitClass)
        {
            _unitClassRepository.Update(unitClass);
        }
    }
}
