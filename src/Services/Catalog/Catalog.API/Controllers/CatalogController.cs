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
    }
}
