using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Data;
using ShopBridge.IRepositories;
using ShopBridge.Models;
using ShopBridge.Models.DTOS;
using ShopBridge.Repositories;
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
        public async void GetAllProductList()
        {
            var productrepo = new Mock<IProductRepository>();
            
            productrepo.Setup(m=>m.ShowProductList()).Returns(GetAllProductMock());

            var classThatweAretesting = new InventoryController(productrepo.Object);

            var prod = await classThatweAretesting.GetProductList();
            var actual = prod.ToList();
            Assert.Equal(4,actual.Count);
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

        [Fact]
        public async void DeleteProduct_Tests()
        {
            var productrepo = new Mock<IProductRepository>();
            productrepo.Setup(m=>m.RemoveProduct(2)).Returns(async ()=>{await Task.Delay(1);return true;});
            var classThatweAretesting = new InventoryController(productrepo.Object);

            //but we will provide 43 instead of 2 to check if it returns false
            var produ =  await classThatweAretesting.DeleteProduct(43) as ObjectResult;
            var response = produ.Value;
            Assert.Equal(false,response);
        }

        [Fact]
        public async void UpdateProduct_Tests()
        {
            var productrepo = new Mock<IProductRepository>();
            var prod = new Product{ProductName = "Chair",Description ="Small",Price = 1000};
            productrepo.Setup(m=>m.ModifyProduct(It.IsAny<int>(),It.IsAny<Product>()))
            .Returns(async ()=>
            {
                await Task.Delay(1);
                return prod;
            });

            var classThatweAretesting = new InventoryController(productrepo.Object);

            var produ = await classThatweAretesting.UpdateProduct(2, prod);
            var response = produ as BadRequestObjectResult;

            Assert.Equal("Description should be atleast 10 letters long",response.Value);
        }


        private async Task<List<Product>> GetAllProductMock()
        {
            await Task.Delay(1);
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
