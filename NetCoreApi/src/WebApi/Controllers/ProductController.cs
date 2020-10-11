using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Audit.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreExampleAuth.Domain.Core;
using NetCoreExampleAuth.Domain.Core.Model;
using NetCoreExampleAuth.Models.Common;

namespace NetCoreExampleAuth.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public ProductController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(uow.Products.GetAllProducts());
        }

        /// <summary>
        /// Get product by Id
        /// </summary>
        /// <param name="id" example="2">Id</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = this.uow.Products.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            if (id < 1)
            {
                throw new Exception("Bad id!"); // TODO: do something with anonymous exceptions.
            }

            return product;
        }

        /// <summary>
        /// Add new Product
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Product
        ///     {
        ///        "name": "Item1",
        ///        "IsGoodQuality": true,
        ///        "Color": "Red"
        ///     }
        ///     or
        ///     {
        ///        "name": "Item1",
        ///        "IsGoodQuality": true,
        ///        "Color": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="product">New product</param>
        /// <returns>Id of newly created Product</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or bad validating</response>      
        [HttpPost]
        [Authorize(Roles = "Administrator,Moderator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)] // TODO: Make ProducesResponseType Global Parameter or Automate
        public IActionResult Post([FromBody] Product product)
        {
            if (!product.IsGoodQuality)
            {
                return BadRequest("You can add only good quality products to our store!"); //TODO: ProblemDetails factory
            }

            var addedProduct = this.uow.Products.AddProduct(product);
            this.uow.Complete();

            return CreatedAtAction(nameof(GetProductById), new { id = addedProduct.Id }, addedProduct);

            // not best practise return 201 without location header
            //return StatusCode(StatusCodes.Status201Created, product); 
        }
    }
}