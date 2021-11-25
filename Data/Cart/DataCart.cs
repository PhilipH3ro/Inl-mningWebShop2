using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Model;

namespace DC2.UI.Data
{
    public class DataCart : IDataCart
    {
        public List<CartDTO> GetAll()
        {
            var carts = LoadCarts();
            return (from cust in carts
                    orderby cust.CartId
                    select cust).ToList();
        }

        public CartDTO GetById(int id, int productId)
        {
            var carts = LoadCarts();
            return carts.FirstOrDefault(x => (x.CartId == id) && (x.ProductId == productId));
        }
        public CartDTO GetById(int id)
        {
            var carts = LoadCarts();
            return carts.FirstOrDefault(x => (x.CartId == id));
        }

        public void RemoveCart(CartDTO removeCart)
        {
            var carts = LoadCarts();
            var cart = carts.FirstOrDefault(x => (x.CartId == removeCart.CartId) && (x.ProductId == removeCart.ProductId));
            carts.Remove(cart);

            SaveCarts(carts);
        }
        public void AddCart(CartDTO removeCart)
        {
            var carts = LoadCarts();
            var cart = carts.FirstOrDefault(x => (x.CartId == removeCart.CartId) && (x.ProductId == removeCart.ProductId));
            carts.Remove(cart);

            SaveCarts(carts);
        }
        public void UpdateCart(CartDTO newUserDetails)
        {
            var carts = LoadCarts();

            var cart = carts.FirstOrDefault(x => x.CartId == newUserDetails.CartId);
            if (cart is not null)
            {
                carts.Remove(cart);
                carts.Add(newUserDetails);
            }
            carts = (from cust in carts
                     orderby cart.CartId
                     select cust).ToList();

            SaveCarts(carts);
        }
        public void AddToCart(int id, int productId)
        {
            var carts = LoadCarts();

            var cartsToUpdate = carts.Where(x => x.CartId == id).ToList();

            var cartToUpdate = cartsToUpdate.FirstOrDefault(x => x.ProductId == productId);

            if (cartToUpdate is not null)
            {
                var tmp = cartToUpdate;
                cartsToUpdate.Remove(cartToUpdate);
                tmp.Quantity++;
                cartsToUpdate.Add(tmp);
                carts.RemoveAll(x => x.CartId == id);
                carts.AddRange(cartsToUpdate);
            }
            else
            {
                carts.Add(new CartDTO() { CartId = id, ProductId = productId, Quantity = 1 });
            }

            SaveCarts(carts);
        }

        public int GetNewId()
        {
            var carts = LoadCarts();
            var max = carts.Max(x => x.CartId);
            return max + 1;
        }

        public List<CartDTO> LoadCarts()
        {
            var path = @"Json\Cart.json";
            var jsonResponse = File.ReadAllText(path);
            var tmp = JsonConvert.DeserializeObject<IEnumerable<CartDTO>>(jsonResponse);
            return tmp.ToList();
        }

        public void SaveCarts(List<CartDTO> carts)
        {
            var path = @"Json\Cart.json";
            var jsonResponse = JsonConvert.SerializeObject(carts);
            File.WriteAllText(path, jsonResponse);
        }
    }
}
