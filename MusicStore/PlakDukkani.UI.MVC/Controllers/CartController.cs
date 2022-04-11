using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlakDukkani.UI.MVC.Helpers;
using PlakDukkani.BLL.Abstract;
using PlakDukkani.BLL.Concrete;
using PlakDukkani.BLL.Concrete.ResultServiceBLL;
using PlakDukkani.ViewModel.Constraints;
using PlakDukkani.ViewModel.CartViewModels.CartItems;

namespace PlakDukkani.UI.MVC.Controllers
{
    public class CartController : Controller
    {
        IAlbumBLL albumService;
        public CartController(IAlbumBLL albumService)
        {
            this.albumService = albumService;
        }

        public IActionResult Index() //Sepet Sayfası 
        {
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            if (cart == null)
            {
                ViewBag.Message = CartMessage.CartBosHatasi;
                return View();
            }
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            if (cart == null)
            {
                cart = new Cart(); //cart:sepet, sepet null ise yeni sepet oluştur değilse aşağıda sepeti doldur.
            }

            ResultService<CartItem> result = albumService.GetCartById(id);
            if (!result.HasError)
            {
                CartItem item = result.Data;
                item.Quantity = 1;
                cart.Add(item);
                HttpContext.Session.Set<Cart>("cart", cart); //Sessiona sepeti gönderiyoruz.
            }
            return PartialView("_cartButton", cart.TotalQuantity);
        }

        //public IActionResult GetCartButton() 
        //{
        //    Cart cart = new Cart();
        //    if (HttpContext.Session.Get<Cart>("cart") != null)
        //    {
        //        cart = HttpContext.Session.Get<Cart>("cart");
        //    }
        //    return PartialView("_cartButton", cart.TotalQuantity);
        //}

        public IActionResult UpdateToCart(int id, short quantity)
        {
            Cart cart = HttpContext.Session.Get<Cart>("cart");
            cart.Update(id, quantity);
            HttpContext.Session.Set<Cart>("cart", cart);
            return PartialView("_cartTable", cart);
        }

    }
}
