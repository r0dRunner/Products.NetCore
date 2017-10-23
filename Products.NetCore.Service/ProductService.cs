using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Products.NetCore.Entity;
using Products.NetCore.Model;
using Products.NetCore.Repository.Interfaces;
using Products.NetCore.Service.Helpers.Exceptions;
using Products.NetCore.Service.Interfaces;

namespace Products.NetCore.Service
{
    public class ProductService : IProductService
    {
        #region Properties
        private readonly IProductRepository _productRepository;
        #endregion

        #region Constructors
        public ProductService(
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<ProductModel>> RetrieveAsync()
        {
            var productEntities = await _productRepository.RetrieveAsync();
            var productModels = Mapper.Map<IEnumerable<ProductModel>>(productEntities);

            return productModels;
        }

        public async Task<ProductModel> RetrieveByIdAsync(Guid id)
        {
            var productEntity = await _productRepository.RetrieveByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"No product found with Id {id}.");
            }

            var productModel = Mapper.Map<ProductModel>(productEntity);

            return productModel;
        }

        public async Task<IEnumerable<ProductModel>> RetrieveByNameAsync(string name)
        {
            var productEntities = await _productRepository.RetrieveByNameAsync(name);
            var productModels = Mapper.Map<IEnumerable<ProductModel>>(productEntities);

            return productModels;
        }

        public async Task<ProductModel> CreateAsync(ProductModel product)
        {
            var productEntity = Mapper.Map<ProductEntity>(product);

            productEntity = await _productRepository.CreateAsync(productEntity);

            var productModel = Mapper.Map<ProductModel>(productEntity);

            return productModel;
        }

        public async Task UpdateAsync(Guid id, ProductModel product)
        {
            var productEntity = await _productRepository.RetrieveByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"No product found with Id {id}.");
            }

            Mapper.Map(product, productEntity);
            productEntity.Id = id;

            await _productRepository.UpdateAsync(productEntity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var productEntity = await _productRepository.RetrieveByIdAsync(id);
            if (productEntity == null)
            {
                throw new NotFoundException($"No product found with Id {id}.");
            }

            await _productRepository.DeleteAsync(id);
        }
        #endregion
    }
}
