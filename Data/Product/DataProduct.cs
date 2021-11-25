using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DC2.UI.Model;

namespace DC2.UI.Data
{
    public class DataProduct : IDataProduct
    {
        public List<ProductDTO> GetAll()
        {
            var products = LoadProducts();
            return (from cust in products
                    orderby cust.ProductId
                    select cust).ToList();
        }

        public ProductDTO GetById(int id)
        {
            var products = LoadProducts();
            return products.FirstOrDefault(x => x.ProductId == id);
        }

        public void AddProduct(ProductDTO product)
        {
            var products = LoadProducts();
            product.ProductId = GetNewId();
            products.Add(product);

            SaveProducts(products);
        }
        public void RemoveProduct(ProductDTO removeProduct)
        {
            var products = LoadProducts();
            var product = products.FirstOrDefault(x => x.ProductId == removeProduct.ProductId);
            products.Remove(product);

            SaveProducts(products);
        }
        public void UpdateProduct(ProductDTO newUserDetails)
        {
            var products = LoadProducts();

            var product = products.FirstOrDefault(x => x.ProductId == newUserDetails.ProductId);
            if (product is not null)
            {
                products.Remove(product);
                products.Add(newUserDetails);
            }
            products = (from cust in products
                        orderby product.ProductId
                        select cust).ToList();

            SaveProducts(products);
        }

        private int GetNewId()
        {
            var products = LoadProducts();
            var max = products.Max(x => x.ProductId);
            return max + 1;
        }


        private List<ProductDTO> LoadProducts()
        {
            var path = @"Json\Product.json";
            var jsonResponse = File.ReadAllText(path);
            var tmp = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(jsonResponse);
            return tmp.ToList();
        }

        private void SaveProducts(List<ProductDTO> products)
        {
            var path = @"Json\Product.json";
            var jsonResponse = JsonConvert.SerializeObject(products);
            File.WriteAllText(path, jsonResponse);
        }
    }
}
