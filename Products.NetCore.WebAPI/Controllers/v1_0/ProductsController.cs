using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.NetCore.Model;
using Products.NetCore.Service.Interfaces;
using Products.NetCore.WebAPI.Helpers.Filters;
using Products.NetCore.WebAPI.DTOs;

namespace Products.NetCore.WebAPI.Controllers.v1_0
{
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        #region Properties
        private readonly IProductService _productService;
        private readonly IProductOptionService _productOptionService;
        #endregion

        #region Constructors
        public ProductsController(
            IProductService productService,
            IProductOptionService productOptionService)
        {
            _productService = productService;
            _productOptionService = productOptionService;
        }
        #endregion

        #region Methods - Product
        // GET /products - gets all products.
        // GET /products?name={name} - finds all products matching the specified name.
        [HttpGet("")]
        public async Task<IActionResult> RetrieveProductsByName(string name)
        {
            var productsModel = string.IsNullOrEmpty(name)
                ? await _productService.RetrieveAsync()
                : await _productService.RetrieveByNameAsync(name);
            var productCollectionDTO = Mapper.Map<CollectionDTO<ProductDTO>>(productsModel);

            return Ok(productCollectionDTO);
        }

        // GET /products/{id} - gets the project that matches the specified ID - ID is a GUID.
        [HttpGet("{id:guid}", Name = "RetrieveProductById")]
        public async Task<IActionResult> RetrieveProductById(Guid id)
        {
            var productModel = await _productService.RetrieveByIdAsync(id);
            var productDTO = Mapper.Map<ProductDTO>(productModel);

            return Ok(productDTO);
        }

        // POST /products - creates a new product.
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody]ProductDTO productDTO)
        {
            var productModel = Mapper.Map<ProductModel>(productDTO);
            productModel = await _productService.CreateAsync(productModel);
            productDTO = Mapper.Map<ProductDTO>(productModel);

            return CreatedAtRoute("RetrieveProductById", new { id = productDTO.Id }, productDTO);
        }

        // PUT /products/{id} - updates a product.
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody]ProductDTO productDTO)
        {
            var productModel = Mapper.Map<ProductModel>(productDTO);
            await _productService.UpdateAsync(id, productModel);

            return NoContent();
        }

        // DELETE /products/{id} - deletes a product and its options.
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteAsync(id);

            return NoContent();
        }
        #endregion

        #region Methods - ProductOption

        // GET /products/{id}/options - finds all options for a specified product.
        [HttpGet("{id:guid}/options")]
        public async Task<IActionResult> RetrieveProductOptionsByProductId(Guid id)
        {
            var productOptionsModel = await _productOptionService.RetrieveByProductIdAsync(id);
            var productOptionsCollectionDTO = Mapper.Map<CollectionDTO<ProductOptionDTO>>(productOptionsModel);

            return Ok(productOptionsCollectionDTO);
        }

        // GET /products/{id}/options/{optionId} - finds the specified product option for the specified product.
        [HttpGet("{id:guid}/options/{optionId:guid}", Name = "RetrieveProductOptionByProductIdAndOptionId")]
        public async Task<IActionResult> RetrieveProductOptionByProductIdAndOptionId(Guid id, Guid optionId)
        {
            var productOptionModel = await _productOptionService.RetrieveByProductIdAndIdAsync(id, optionId);
            var productOptionDTO = Mapper.Map<ProductOptionDTO>(productOptionModel);

            return Ok(productOptionDTO);
        }

        // POST /products/{id}/options - adds a new product option to the specified product.
        [HttpPost("{id:guid}/options")]
        public async Task<IActionResult> CreateProductOption(Guid id, [FromBody]ProductOptionDTO productOptionDTO)
        {
            var productOptionModel = Mapper.Map<ProductOptionModel>(productOptionDTO);
            productOptionModel = await _productOptionService.CreateAsync(id, productOptionModel);
            productOptionDTO = Mapper.Map<ProductOptionDTO>(productOptionModel);

            return CreatedAtRoute(
                "RetrieveProductOptionByProductIdAndOptionId",
                new { id = id, optionId = productOptionDTO.Id },
                productOptionDTO);
        }

        // PUT /products/{id}/options/{optionId} - updates the specified product option.
        [HttpPut("{id:guid}/options/{optionId:guid}")]
        public async Task<IActionResult> UpdateProductOption(Guid id, Guid optionId, [FromBody]ProductOptionDTO productOptionDTO)
        {
            var productOptionModel = Mapper.Map<ProductOptionModel>(productOptionDTO);
            await _productOptionService.UpdateAsync(id, optionId, productOptionModel);

            return NoContent();
        }

        // DELETE /products/{id}/options/{optionId} - deletes the specified product option.
        [HttpDelete("{id:guid}/options/{optionId:guid}")]
        public async Task<IActionResult> DeleteProductOption(Guid id, Guid optionId)
        {
            await _productOptionService.DeleteAsync(id, optionId);

            return NoContent();
        }
        #endregion
    }
}
