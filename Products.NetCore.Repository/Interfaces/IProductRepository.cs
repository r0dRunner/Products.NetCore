using System.Collections.Generic;
using System.Threading.Tasks;
using Products.NetCore.Entity;

namespace Products.NetCore.Repository.Interfaces
{
    public interface IProductRepository: IRepository<ProductEntity>
    {
        Task<IEnumerable<ProductEntity>> RetrieveByNameAsync(string name);
    }
}
