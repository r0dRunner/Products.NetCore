using System;

namespace Products.NetCore.Entity
{
    public class ProductEntity : BaseEntity
    {

        public override Guid Id { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

    }
}

