﻿using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogService _catalogServce;
        private readonly IBasketService _basketService;

        public IndexModel(ICatalogService catalogServce, IBasketService basketService)
        {
            _catalogServce = catalogServce ?? throw new ArgumentNullException(nameof(catalogServce));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _catalogServce.GetCatalog();
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
