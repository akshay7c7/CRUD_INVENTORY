using System;
using System.Security.AccessControl;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using ShopBridge.Models;

namespace ShopBridge.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context)
        {
            if(!context.Products.Any())
            {
                var prodData = System.IO.File.ReadAllText("Data/ProductSeedData.json");
                var users = JsonConvert.DeserializeObject<List<Product>>(prodData);
                foreach (var user in users)
                {
                    context.Products.Add(user);
                }

                context.SaveChanges();
            }

        }
    }
}