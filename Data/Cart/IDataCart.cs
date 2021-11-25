using DC2.UI.Model;
using System.Collections.Generic;

namespace DC2.UI.Data
{
    public interface IDataCart
    {
        void AddToCart(int id, int productId);
        List<CartDTO> GetAll();
        CartDTO GetById(int id);
        CartDTO GetById(int id, int productId);
        int GetNewId();
        List<CartDTO> LoadCarts();
        void RemoveCart(CartDTO removeCart);
        void SaveCarts(List<CartDTO> carts);
        void UpdateCart(CartDTO newUserDetails);
    }
}