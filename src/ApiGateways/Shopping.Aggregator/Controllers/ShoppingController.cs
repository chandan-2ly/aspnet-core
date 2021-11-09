using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogService _catalogService;
        private readonly IOrderService _orderService;

        public ShoppingController(IBasketService basketService, ICatalogService catalogService, 
            IOrderService orderService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            //get basket with username
            //iterate basket items and consume products with basket item productid member
            //map product related members into basketitem dto with extended columns
            //consume ordering microservice in order to retrieve order list
            //return root shopping model dto class 
            var basket = await _basketService.GetBasket(userName);
            foreach(var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }
            var orders = await _orderService.GetOrdersByUserName(userName);
            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}
