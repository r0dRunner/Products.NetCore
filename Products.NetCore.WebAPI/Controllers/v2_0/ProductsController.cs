using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Products.NetCore.WebAPI.Helpers.Filters;
using Products.NetCore.WebAPI.DTOs;

namespace Products.NetCore.WebAPI.Controllers.v2_0
{
    [ApiVersion("2.0")]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        #region Properties
        //private readonly IProductService _productService;
        //private readonly IProductOptionService _productOptionService;
        private readonly string _message = "API Version 2.0 in progress.";
        #endregion

        #region Constructors
        //public ProductsController(
        //    IProductService productService,
        //    IProductOptionService productOptionService)
        //{
        //    _productService = productService;
        //    _productOptionService = productOptionService;
        //}
        #endregion

        #region Methods - Product
        // GET /products - gets all products.
        // GET /products?name={name} - finds all products matching the specified name.
        [Route("")]
        [HttpGet]
        public IActionResult RetrieveProductsByName(string name)
        {
            throw new NotImplementedException(_message);
        }

        // GET /products/{id} - gets the project that matches the specified ID - ID is a GUID.
        [Route("{id:guid}", Name = "RetrieveProductByIdV2_0")]
        [HttpGet]
        public IActionResult RetrieveProductById(Guid id)
        {
            throw new NotImplementedException(_message);
        }

        // POST /products - creates a new product.
        [Route("")]
        [HttpPost]
        public IActionResult CreateProduct([FromBody]ProductDTO productDTO)
        {
            throw new NotImplementedException(_message);
        }

        // PUT /products/{id} - updates a product.
        [Route("{id:guid}")]
        [HttpPut]
        public IActionResult UpdateProduct(Guid id, [FromBody]ProductDTO productDTO)
        {
            throw new NotImplementedException(_message);
        }

        // DELETE /products/{id} - deletes a product and its options.
        [Route("{id:guid}")]
        [HttpDelete]
        public IActionResult DeleteProduct(Guid id)
        {
            throw new NotImplementedException(_message);
        }
        #endregion

        #region Methods - ProductOption

        // GET /products/{id}/options - finds all options for a specified product.
        [Route("{id:guid}/options")]
        [HttpGet]
        public IActionResult RetrieveProductOptionsByProductId(Guid id)
        {
            throw new NotImplementedException(_message);
        }

        // GET /products/{id}/options/{optionId} - finds the specified product option for the specified product.
        [Route("{id:guid}/options/{optionId:guid}", Name = "RetrieveProductOptionsByProductIdAndOptionIdV2_0")]
        [HttpGet]
        public IActionResult RetrieveProductOptionsByProductIdAndOptionId(Guid id, Guid optionId)
        {
            throw new NotImplementedException(_message);
        }

        // POST /products/{id}/options - adds a new product option to the specified product.
        [Route("{id:guid}/options")]
        [HttpPost]
        public IActionResult CreateProductOption(Guid id, [FromBody]ProductOptionDTO productOptionDTO)
        {
            throw new NotImplementedException(_message);
        }

        // PUT /products/{id}/options/{optionId} - updates the specified product option.
        [Route("{id:guid}/options/{optionId:guid}")]
        [HttpPut]
        public IActionResult UpdateProductOption(Guid id, Guid optionId, [FromBody]ProductOptionDTO productOptionDTO)
        {
            throw new NotImplementedException(_message);
        }

        // DELETE /products/{id}/options/{optionId} - deletes the specified product option.
        [Route("{id:guid}/options/{optionId:guid}")]
        [HttpDelete]
        public IActionResult DeleteProductOption(Guid id, Guid optionId)
        {
            throw new NotImplementedException(_message);
        }
        #endregion
    }
}
