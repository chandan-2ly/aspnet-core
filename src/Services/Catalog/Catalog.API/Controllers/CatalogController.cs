using Catalog.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ICatalogService catalogService, ILogger<CatalogController> logger)
        {
            _catalogService = catalogService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result = await _catalogService.GetProducts();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _catalogService.CreateProduct(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _catalogService.UpdateProduct(product));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _catalogService.DeleteProduct(id));
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var result = await _catalogService.GetProductById(id);

                if (result == null)
                {
                    _logger.LogError($"Product with id: {id}, not found.");
                    return NotFound();
                }
                return Ok(result);
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet]
        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                var result = await _catalogService.GetProductByCategory(category);

                if (result == null)
                {
                    _logger.LogError($"Products with category: {category}, not found.");
                    return NotFound();
                }
                return Ok(result);
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var result = await _catalogService.GetProductByName(name);

                if (result == null)
                {
                    _logger.LogError($"Product with name: {name}, not found.");
                    return NotFound();
                }
                return Ok(result);
            }
            return BadRequest("Invalid Input");
        }
    }
}
