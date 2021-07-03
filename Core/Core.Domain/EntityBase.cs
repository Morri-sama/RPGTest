using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class EntityBase : EntityBase<string>
    {

    }
}
