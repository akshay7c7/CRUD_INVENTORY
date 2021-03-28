using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Data;
using ShopBridge.IRepositories;
using ShopBridge.Models;

namespace ShopBridge.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;
        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Product> SaveProduct(Product product)
        {
            await _dataContext.Products.AddAsync(product);
            await _dataContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var productToDelete = await _dataContext.Products.FirstOrDefaultAsync(x=>x.ProductId==id);
            _dataContext.Products.Remove(productToDelete);
            return await _dataContext.SaveChangesAsync()>0;

        }

        public async Task<Product> ShowProduct(int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x=>x.ProductId==id);
            return product;
        }

        public async Task<List<Product>> ShowProductList()
        {
            return await _dataContext.Products.ToListAsync();
        }

        public async Task<Product> ModifyProduct(int id, Product product)
        {
            var productToUpdate = await _dataContext.Products.FirstOrDefaultAsync(x=>x.ProductId==id);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;

            await _dataContext.SaveChangesAsync();
            
            return productToUpdate;
            
        }

    }
}