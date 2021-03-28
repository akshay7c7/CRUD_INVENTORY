using Microsoft.EntityFrameworkCore;
using ShopBridge.Models;

namespace ShopBridge.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options): base(options){}
        public DbSet<Product> Products{get; set;}
    }
}