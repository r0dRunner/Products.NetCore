using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Products.NetCore.Model;

namespace Products.NetCore.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> RetrieveAsync();

        Task<IEnumerable<ProductModel>> RetrieveByNameAsync(string name);

        Task<ProductModel> RetrieveByIdAsync(Guid id);

        Task<ProductModel> CreateAsync(ProductModel product);

        Task UpdateAsync(Guid id, ProductModel product);

        Task DeleteAsync(Guid id);
    }
}
