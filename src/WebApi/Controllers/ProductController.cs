using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAxampleAuth.Patterns.Models.Product;
using NetCoreExampleAuth.Models.Common;

namespace NetCoreExampleAuth.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // mockup
        private IList<Product> products = new List<Product> {
                new Product() {
                    Id = 1,
                    Name = "Bike",
                    Color = ProductColor.Blue
                },
                new Product() {
                    Id = 2,
                    Name = "Car",
                    Color = ProductColor.Red
                },
                new Product() {
                    Id = 3,
                    Name = "Car",
                    Color = ProductColor.Green,
                    IsGoodQuality = true
                },
            };

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Products</returns>
        [Authorize(Roles ="Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(products.AsEnumerable());
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
            var result = products.FirstOrDefault(p => p.Id == id);

            if (id < 1)
            {
                throw new Exception("Bad id!"); // TODO: do something with anonymous exceptions.
            }

            if (result is null)
            {
                return NotFound();
            }

            return result;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)] // TODO: Make ProducesResponseType Global Parameter or Automate
        public IActionResult Post([FromBody] Product product)
        {
            if (!product.IsGoodQuality)
            {
                return BadRequest("You can add only good quality products to our store!"); //TODO: ProblemDetails factory
            }

            product.Id = products.Count + 1;
            products.Add(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);

            // not best practise return 201 without location header
            //return StatusCode(StatusCodes.Status201Created, product); 
        }
    }
}