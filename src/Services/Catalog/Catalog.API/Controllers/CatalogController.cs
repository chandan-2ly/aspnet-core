using Catalog.API.Entities;
using Catalog.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WebApiResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts()
        {
            var webApiResponse = new WebApiResponseModel();
            
            var result = await _catalogService.GetProducts();
            
            if(result != null)
            {
                webApiResponse.IsSuccess = true;
                webApiResponse.Data = result;
            }

            return Ok(webApiResponse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _catalogService.CreateProduct(product);
            var webApiResponse = new WebApiResponseModel();
            webApiResponse.IsSuccess = true;
            webApiResponse.Data = product;
            return Ok(webApiResponse);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _catalogService.UpdateProduct(product));
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await _catalogService.DeleteProduct(id));
        }

        [HttpGet("GetProductById")]
        [ProducesResponseType(typeof(WebApiResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductById(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var webApiResponse = new WebApiResponseModel();
                var result = await _catalogService.GetProductById(id);

                if (result != null)
                {
                    webApiResponse.IsSuccess = true;
                    webApiResponse.Data = result;
                    return Ok(webApiResponse);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet]
        [Route("GetProductByCategory")]
        [ProducesResponseType(typeof(WebApiResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByCategory(string category)
        {
            if (!string.IsNullOrEmpty(category))
            {
                var webApiResponse = new WebApiResponseModel();
                var result = await _catalogService.GetProductByCategory(category);

                if (result != null)
                {
                    webApiResponse.IsSuccess = true;
                    webApiResponse.Data = result;
                    return Ok(webApiResponse);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet]
        [Route("GetProductByName")]
        [ProducesResponseType(typeof(WebApiResponseModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var webApiResponse = new WebApiResponseModel();
                var result = await _catalogService.GetProductByName(name);

                if (result != null)
                {
                    webApiResponse.IsSuccess = true;
                    webApiResponse.Data = result;
                    return Ok(webApiResponse);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest("Invalid Input");
        }
    }
}
