using System;
using System.Collections.Generic;
using System.Text;

namespace Products.NetCore.Entity
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
