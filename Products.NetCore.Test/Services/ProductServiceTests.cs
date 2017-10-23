using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Products.NetCore.Entity;
using Products.NetCore.Model;
using Products.NetCore.Repository.Interfaces;
using Products.NetCore.Service;
using Products.NetCore.Service.Helpers.Exceptions;
using Products.NetCore.Service.Interfaces;
using RandomTestValues;

namespace Products.NetCore.Test.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _productRepository;
        private Mock<IProductOptionRepository> _productOptionRepository;
        private IProductOptionService _productOptionService;

        [TestInitialize]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();
            _productOptionRepository = new Mock<IProductOptionRepository>();
            _productOptionService = new ProductOptionService(
                _productRepository.Object, _productOptionRepository.Object);
            Mapper.Initialize(a => a.AddProfiles(typeof(ProductOptionService)));
        }

        [TestMethod]
        public async Task RetrieveByProductIdAsync_WithMatchingEntities_ShouldReturnEquivalentModels()
        {
            var entities = RandomValue.IList<ProductOptionEntity>(2);
            var productId = RandomValue.Guid();
            _productOptionRepository.Setup(a => a.RetrieveByProductIdAsync(productId)).ReturnsAsync(entities);

            var models = await _productOptionService.RetrieveByProductIdAsync(productId);

            Assert.IsNotNull(models);
            Assert.IsInstanceOfType(models, typeof(IEnumerable<ProductOptionModel>));
            
            var productOptionModels = models as IList<ProductOptionModel> ?? models.ToList();
            Assert.AreEqual(entities.Count, productOptionModels.Count);
            Assert.AreEqual(entities.FirstOrDefault()?.Id, productOptionModels.FirstOrDefault()?.Id);
            Assert.AreEqual(entities.FirstOrDefault()?.Name, productOptionModels.FirstOrDefault()?.Name);
            Assert.AreEqual(entities.FirstOrDefault()?.Description, productOptionModels.FirstOrDefault()?.Description);
            Assert.AreEqual(entities.FirstOrDefault()?.ProductId, productOptionModels.FirstOrDefault()?.ProductId);
        }

        [TestMethod]
        public async Task RetrieveByProductIdAsync_WithNoMatchingEntities_ShouldReturnEmptyList()
        {
            var entities = RandomValue.IList<ProductOptionEntity>(0);
            var productId = RandomValue.Guid();
            _productOptionRepository.Setup(a => a.RetrieveByProductIdAsync(productId)).ReturnsAsync(entities);

            var models = await _productOptionService.RetrieveByProductIdAsync(productId);

            Assert.IsNotNull(models);
            Assert.IsInstanceOfType(models, typeof(IEnumerable<ProductOptionModel>));

            var productOptionModels = models as IList<ProductOptionModel> ?? models.ToList();
            Assert.AreEqual(entities.Count, productOptionModels.Count);
            Assert.AreEqual(0, productOptionModels.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task RetrieveByProductIdAndIdAsync_WithNoMatchingEntity_ShouldThrowNotFoundException()
        {
            var id = RandomValue.Guid();
            var productId = RandomValue.Guid();
            _productOptionRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductOptionEntity));

            await _productOptionService.RetrieveByProductIdAndIdAsync(productId, id);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task RetrieveByProductIdAndIdAsync_WithMatchingEntityButDiffProductId_ShouldThrowNotFoundException()
        {
            var entity = RandomValue.Object<ProductOptionEntity>();
            var id = RandomValue.Guid();
            var productId = RandomValue.Guid();
            while (productId == entity.ProductId) { productId = RandomValue.Guid(); }
            _productOptionRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(entity);

            await _productOptionService.RetrieveByProductIdAndIdAsync(productId, id);
        }

        [TestMethod]
        public async Task RetrieveByProductIdAndIdAsync_WithMatchingEntity_ShouldReturnEquivalentModel()
        {
            var entity = RandomValue.Object<ProductOptionEntity>();
            var id = RandomValue.Guid();
            var productId = entity.ProductId;
            _productOptionRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(entity);

            var model = await _productOptionService.RetrieveByProductIdAndIdAsync(productId, id);

            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(ProductOptionModel));
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Name, model.Name);
            Assert.AreEqual(entity.Description, model.Description);
            Assert.AreEqual(entity.ProductId, model.ProductId);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task CreateAsync_WithNoMatchingProductEntity_ShouldThrowNotFoundException()
        {
            var model = RandomValue.Object<ProductOptionModel>();
            var productId = RandomValue.Guid();
            var id = RandomValue.Guid();
            _productRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductEntity));

            await _productOptionService.CreateAsync(productId, model);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task UpdateAsync_WithNoMatchingEntity_ShouldThrowNotFoundException()
        {
            var model = RandomValue.Object<ProductOptionModel>();
            var productId = RandomValue.Guid();
            var id = RandomValue.Guid();
            _productOptionRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductOptionEntity));

            await _productOptionService.UpdateAsync(productId, id, model);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task DeleteAsync_WithNoMatchingEntity_ShouldThrowNotFoundException()
        {
            var productId = RandomValue.Guid();
            var id = RandomValue.Guid();
            _productOptionRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductOptionEntity));

            await _productOptionService.DeleteAsync(productId, id);
        }
    }
}
