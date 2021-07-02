﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Infrastructure
{
    public interface IRepository<T> where T : EntityBase
    {
        public T GetById(long id);
        public void Insert(T entity);
        public void Update(T entity);
        public void Delete(T entity);

        public ICollection<T> Get(Expression<Func<T, bool>> predicate);
        public ICollection<T> Get();
    }
}
