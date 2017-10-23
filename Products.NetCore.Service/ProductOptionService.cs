using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Products.NetCore.Entity;
using Products.NetCore.Model;
using Products.NetCore.Repository.Interfaces;
using Products.NetCore.Service.Helpers.Exceptions;
using Products.NetCore.Service.Interfaces;

namespace Products.NetCore.Service
{
    public class ProductOptionService : IProductOptionService
    {
        #region Properties
        private readonly IProductRepository _productRepository;
        private readonly IProductOptionRepository _productOptionRepository;
        #endregion

        #region Constructors
        public ProductOptionService(
            IProductRepository productRepository,
            IProductOptionRepository productOptionRepository)
        {
            _productRepository = productRepository;
            _productOptionRepository = productOptionRepository;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<ProductOptionModel>> RetrieveByProductIdAsync(Guid productId)
        {
            var productOptionEntities = await _productOptionRepository.RetrieveByProductIdAsync(productId);
            var productOptionModels = Mapper.Map<IEnumerable<ProductOptionModel>>(productOptionEntities);

            return productOptionModels;
        }

        public async Task<ProductOptionModel> RetrieveByProductIdAndIdAsync(Guid productId, Guid id)
        {
            var productOptionEntity = await _productOptionRepository.RetrieveByIdAsync(id);
            if (productOptionEntity == null || productOptionEntity.ProductId != productId)
            {
                throw new NotFoundException($"No product option found with Id {id} and ProductId {productId}.");
            }

            var productOptionModel = Mapper.Map<ProductOptionModel>(productOptionEntity);

            return productOptionModel;
        }

        public async Task<ProductOptionModel> CreateAsync(Guid productId, ProductOptionModel productOption)
        {
            var productEntity = await _productRepository.RetrieveByIdAsync(productId);
            if (productEntity == null)
            {
                throw new NotFoundException($"No product found with Id {productId}.");
            }

            var productOptionEntity = Mapper.Map<ProductOptionEntity>(productOption);
            productOptionEntity.ProductId = productId;

            productOptionEntity = await _productOptionRepository.CreateAsync(productOptionEntity);

            var productOptionModel = Mapper.Map<ProductOptionModel>(productOptionEntity);

            return productOptionModel;
        }

        public async Task UpdateAsync(Guid productId, Guid id, ProductOptionModel productOption)
        {
            var productOptionEntity = await _productOptionRepository.RetrieveByIdAsync(id);
            if (productOptionEntity == null || productOptionEntity.ProductId != productId)
            {
                throw new NotFoundException($"No product option found with Id {id} and ProductId {productId}.");
            }

            Mapper.Map(productOption, productOptionEntity);
            productOptionEntity.Id = id;
            productOptionEntity.ProductId = productId;

            await _productOptionRepository.UpdateAsync(productOptionEntity);
        }

        public async Task DeleteAsync(Guid productId, Guid id)
        {
            var productOptionEntity = await _productOptionRepository.RetrieveByIdAsync(id);
            if (productOptionEntity == null || productOptionEntity.ProductId != productId)
            {
                throw new NotFoundException($"No product option found with Id {id} and ProductId {productId}.");
            }

            await _productOptionRepository.DeleteAsync(id);
        }
        #endregion
    }
}
