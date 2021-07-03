using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure
{
    public interface IMongoDBRepository<T, TId> : IRepository<T, TId> where T : EntityBase
    {

    }

    public interface IMongoDBRepository<T> : IMongoDBRepository<T, string> where T : EntityBase
    {
         
    }
}
