using System;
using System.Collections.Generic;
using System.Text;

namespace IFLike.Domain
{
    public abstract class EntityBaseId<T> : EntityBase
    {
        public T Id { get; set; }
    }
}
