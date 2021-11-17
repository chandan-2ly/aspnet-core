using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogService _catalogServce;
        private readonly IBasketService _basketService;

        public ProductDetailModel(ICatalogService catalogServce, IBasketService basketService)
        {
            _catalogServce = catalogServce;
            _basketService = basketService;
        }

        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogServce.GetCatalog(productId); ;
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogServce.GetCatalog(productId);

            var basket = await _basketService.GetBasket("chandan");
            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
                Color = "Black"
            });
            var basketUpdated = await _basketService.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}