using System;
using System.Collections.Generic;
using System.Text;

namespace Products.NetCore.Entity
{
    public class ProductOptionEntity : BaseEntity
    {
        public override Guid Id { get; set; }

        public Guid ProductId { get; set; }

    }
}
