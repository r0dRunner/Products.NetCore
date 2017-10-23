using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Products.NetCore.WebAPI.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public override Guid Id { get; set; }

        public override string Name { get; set; }

        public override string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var result = base.Validate(context);

            //Validate Price

            //Validate DeliveryPrice

            return result;
        }
    }
}
