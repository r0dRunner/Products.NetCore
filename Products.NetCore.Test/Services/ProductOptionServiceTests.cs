using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ProductOptionServiceTests
    {
        private Mock<IProductRepository> _productRepository;
        private IProductService _productService;

        [TestInitialize]
        public void Setup()
        {
            _productRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepository.Object);
            Mapper.Initialize(a => a.AddProfiles(typeof(ProductService)));
        }

        [TestMethod]
        public async Task RetrieveAsync_WithMatchingEntities_ShouldReturnEquivalentModels()
        {
            var entities = RandomValue.IList<ProductEntity>(2);
            _productRepository.Setup(a => a.RetrieveAsync()).ReturnsAsync(entities);

            var models = await _productService.RetrieveAsync();

            Assert.IsNotNull(models);
            Assert.IsInstanceOfType(models, typeof(IEnumerable<ProductModel>));

            var productModels = models as IList<ProductModel> ?? models.ToList();
            Assert.AreEqual(entities.Count, productModels.Count);
            Assert.AreEqual(entities.FirstOrDefault()?.Id, productModels.FirstOrDefault()?.Id);
            Assert.AreEqual(entities.FirstOrDefault()?.Name, productModels.FirstOrDefault()?.Name);
            Assert.AreEqual(entities.FirstOrDefault()?.Description, productModels.FirstOrDefault()?.Description);
            Assert.AreEqual(entities.FirstOrDefault()?.Price, productModels.FirstOrDefault()?.Price);
            Assert.AreEqual(entities.FirstOrDefault()?.DeliveryPrice, productModels.FirstOrDefault()?.DeliveryPrice);
        }

        [TestMethod]
        public async Task RetrieveAsync_WithNoMatchingEntities_ShouldReturnEmptyList()
        {
            var entities = RandomValue.IList<ProductEntity>(0);
            _productRepository.Setup(a => a.RetrieveAsync()).ReturnsAsync(entities);

            var models = await _productService.RetrieveAsync();

            Assert.IsNotNull(models);
            Assert.IsInstanceOfType(models, typeof(IEnumerable<ProductModel>));

            var productModels = models as IList<ProductModel> ?? models.ToList();
            Assert.AreEqual(entities.Count, productModels.Count);
            Assert.AreEqual(0, productModels.Count);
        }

        [TestMethod]
        public async Task RetrieveByNameAsync_WithMatchingEntities_ShouldReturnEquivalentModels()
        {
            var entities = RandomValue.IList<ProductEntity>(2);
            var name = RandomValue.String();
            _productRepository.Setup(a => a.RetrieveByNameAsync(name)).ReturnsAsync(entities);

            var models = await _productService.RetrieveByNameAsync(name);

            Assert.IsNotNull(models);
            Assert.IsInstanceOfType(models, typeof(IEnumerable<ProductModel>));

            var productModels = models as IList<ProductModel> ?? models.ToList();
            Assert.AreEqual(entities.Count, productModels.Count);
            Assert.AreEqual(entities.FirstOrDefault()?.Id, productModels.FirstOrDefault()?.Id);
            Assert.AreEqual(entities.FirstOrDefault()?.Name, productModels.FirstOrDefault()?.Name);
            Assert.AreEqual(entities.FirstOrDefault()?.Description, productModels.FirstOrDefault()?.Description);
            Assert.AreEqual(entities.FirstOrDefault()?.Price, productModels.FirstOrDefault()?.Price);
            Assert.AreEqual(entities.FirstOrDefault()?.DeliveryPrice, productModels.FirstOrDefault()?.DeliveryPrice);
        }

        [TestMethod]
        public async Task RetrieveByNameAsync_WithNoMatchingEntities_ShouldReturnEmptyList()
        {
            var entities = RandomValue.IList<ProductEntity>(0);
            var name = RandomValue.String();
            _productRepository.Setup(a => a.RetrieveByNameAsync(name)).ReturnsAsync(entities);

            var models = await _productService.RetrieveByNameAsync(name);

            Assert.IsNotNull(models);
            Assert.IsInstanceOfType(models, typeof(IEnumerable<ProductModel>));

            var productModels = models as IList<ProductModel> ?? models.ToList();
            Assert.AreEqual(entities.Count, productModels.Count);
            Assert.AreEqual(0, productModels.Count);
        }

        [TestMethod]
        public async Task RetrieveByIdAsync_WithMatchingEntity_ShouldReturnEquivalentModel()
        {
            var entity = RandomValue.Object<ProductEntity>();
            var id = RandomValue.Guid();
            _productRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(entity);

            var model = await _productService.RetrieveByIdAsync(id);

            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(ProductModel));
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Name, model.Name);
            Assert.AreEqual(entity.Description, model.Description);
            Assert.AreEqual(entity.Price, model.Price);
            Assert.AreEqual(entity.DeliveryPrice, model.DeliveryPrice);
        }


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task RetrieveByIdAsync_WithNoMatchingEntity_ShouldThrowNotFoundException()
        {
            var id = RandomValue.Guid();
            _productRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductEntity));

            await _productService.RetrieveByIdAsync(id);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task UpdateAsync_WithNoMatchingEntity_ShouldThrowNotFoundException()
        {
            var model = RandomValue.Object<ProductModel>();
            var id = RandomValue.Guid();
            _productRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductEntity));

            await _productService.UpdateAsync(id, model);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task DeleteAsync_WithNoMatchingEntity_ShouldThrowNotFoundException()
        {
            var id = RandomValue.Guid();
            _productRepository.Setup(a => a.RetrieveByIdAsync(id)).ReturnsAsync(default(ProductEntity));

            await _productService.DeleteAsync(id);
        }
    }
}
