using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private ProductDBContext db = new ProductDBContext();
        private ShoppingCart shoppingCart;

        //
        // GET: /Cart/

        public ActionResult Index()
        {
            if (Session["cart"] == null){
                Session["cart"] = new ShoppingCart();
            }
            return View(db.Products.ToList());
        }

        public ActionResult AddToCart(int productId, int quantity){
            Product product = db.Products.Find(productId);
            if (quantity != 0){
                shoppingCart = (ShoppingCart) Session["cart"];
                shoppingCart.AddProduct(product, quantity);
                Session["message"] = "Product added to cart";
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewShoppingCart(){
            shoppingCart = (ShoppingCart)Session["cart"];
            if (shoppingCart.countProductsInCart() == 0){
                Session["message"] = "Cart is empty";
                ViewBag.totalPrice = 0;
            }
            else{
                ViewBag.totalPrice = shoppingCart.GetTotalPrice();
            }
            return View(shoppingCart._cartProducts.ToList());
        }

        public ActionResult BuyProductsFromCart(){
            Session["cart"] = null;
            Session["message"] = "Products were brought";
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult BackToProducts(){
            return RedirectToAction("Index");
        }
    }
}