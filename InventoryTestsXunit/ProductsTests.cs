using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.IRepositories;
using ShopBridge.Models;
using ShopBridge.Models.DTOS;
using Xunit;

namespace InventoryTestsXunit
{
    public class ProductsTests
    {
        [Fact]
        public async void CreateProduct_Tests()
        {
            var productrepo = new Mock<IProductRepository>();
            var prod = new Product{ProductName = "Chair",Description ="Some Some some some",Price = 1000};
            var prodDto = new ProductDto{ProductName = "Chair",Description ="Some Some some some",Price = 1000};
            productrepo.Setup(m=>m.SaveProduct(It.IsAny<Product>()))
            .Returns(async ()=>
            {
                await Task.Delay(1);
                return prod;
            });

            var classThatweAretesting = new InventoryController(productrepo.Object);

            var produ = await classThatweAretesting.CreateProduct(prodDto);
            Assert.Equal(prod.ProductName, produ.Value.ProductName);
        }

        [Fact]
        public async void GetProductById_not_found_verification_Tests()
        {
            var productrepo = new Mock<IProductRepository>();
            productrepo.Setup(m=>m.ShowProduct(It.IsAny<int>()));
            var classThatweAretesting = new InventoryController(productrepo.Object);
            var produ =  await classThatweAretesting.GetProduct(43);
            var resposne = (produ.Result as NotFoundObjectResult).Value;
            Assert.Equal("The product you are looking does not exist",resposne);
        }


        private List<Product> GetAllProductMock()
        {
            List<Product> li = new List<Product>()
            {
                new Product{
                    ProductId =1, Description = "Italian Sofa Soft material", ProductName = "Sofa"
                },
                new Product{
                    ProductId =2, Description = "Wooden Chair made of chandan wood", ProductName = "Wooden Chair"
                },
                new Product{
                    ProductId =3, Description = "Utensil Stand made of steel", ProductName = "Utensil Stand"
                },
                new Product{
                    ProductId =4, Description = "Microwave 200 voltage", ProductName = "Microwave"
                },
            };

            return li;
        }
    }
}
