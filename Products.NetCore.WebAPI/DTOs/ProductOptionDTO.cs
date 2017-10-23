using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.NetCore.WebAPI.DTOs
{
    public class ProductOptionDTO : BaseDTO
    {
        public override Guid Id { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

    }
}
