using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTest.Core.Domain
{
    public abstract class EntityBase<TId>
    {
        public virtual TId Id { get; set; }
    }

    public abstract class EntityBase
    {
        public string Id { get; set; }
    }
}
