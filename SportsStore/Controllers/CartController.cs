using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        private Cart cart;

        public CartController(IProductRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        
        public IActionResult AddToCart(int id, string returnUrl)
        {
            Product product = repository.Products.Where(p => p.ID == id).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult RemoveFromCart(int id, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p=>p.ID==id);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public IActionResult Index(string returnUrl) => View(new CartIndexViewModel
        {
            Cart = cart,
            ReturnURL = returnUrl
        });

        public ActionResult AddCout(int id, int count)
        {
            Product product = repository.Products.Where(p => p.ID == id).FirstOrDefault();
            var q = cart.Lines.Where(p => p.Product.ID == product.ID).FirstOrDefault()?.Quantity;
            if (q > count)
                cart.AddItem(product, -1);
            else
                if (q < count)
                cart.AddItem(product, 1);
            return Json(new { success = true });
        }

    }
}