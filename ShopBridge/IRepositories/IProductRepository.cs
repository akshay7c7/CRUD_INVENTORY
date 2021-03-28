using System.Collections.Generic;
using System.Threading.Tasks;
using ShopBridge.Models;

namespace ShopBridge.IRepositories
{
    public interface IProductRepository
    {
                
        Task<Product> ShowProduct(int id);
        Task<List<Product>> ShowProductList();
        Task<Product> SaveProduct(Product product);
        Task<Product> ModifyProduct(int id, Product product);
        Task<bool> RemoveProduct(int id);
    }
}