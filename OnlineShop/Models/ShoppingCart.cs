using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ShoppingCart
    {
        public Dictionary<Product,int> _cartProducts = new Dictionary<Product,int>();

        public void AddProduct(Product product, int quantity){
            _cartProducts.Add(product,quantity);
        }

        public float GetTotalPrice()
        {
            return _cartProducts.Sum(product => product.Value*product.Key.Price);
        }

        public List<KeyValuePair<Product,Int32>> GetProductsFromCart(){
           return _cartProducts.ToList();
        }

        public int countProductsInCart(){
            return _cartProducts.Count();
        }
    }
}