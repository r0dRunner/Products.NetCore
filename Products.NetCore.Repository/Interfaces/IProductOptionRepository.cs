using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Products.NetCore.Entity;

namespace Products.NetCore.Repository.Interfaces
{
    public interface IProductOptionRepository: IRepository<ProductOptionEntity>
    {
        Task<IEnumerable<ProductOptionEntity>> RetrieveByProductIdAsync(Guid productId);
    }
}
