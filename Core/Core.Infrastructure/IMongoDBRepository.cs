using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure
{
    public interface IMongoDBRepository<T> : IRepository<T> where T : EntityBase
    {

    }
}
