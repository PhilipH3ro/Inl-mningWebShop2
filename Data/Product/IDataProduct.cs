using DC2.UI.Model;
using System.Collections.Generic;

namespace DC2.UI.Data
{
    public interface IDataProduct
    {
        void AddProduct(ProductDTO product);
        List<ProductDTO> GetAll();
        ProductDTO GetById(int id);
        void RemoveProduct(ProductDTO removeProduct);
        void UpdateProduct(ProductDTO newUserDetails);
    }
}