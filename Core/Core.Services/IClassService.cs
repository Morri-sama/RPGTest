using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface IClassService
    {
        public ICollection<Class> GetAll();
        public void Insert(Class entity);
        public void Update(Class entity);
        public void Delete(Class entity);
    }
}
