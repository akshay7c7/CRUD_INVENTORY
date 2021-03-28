using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.IRepositories;
using ShopBridge.Models.DTOS;
using System;
using ShopBridge.Models;

namespace ShopBridge.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InventoryController : ControllerBase
    {
        IProductRepository _repo;
        public InventoryController(IProductRepository repo )
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProductList()
        {
            return await _repo.ShowProductList();
        }


        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productToreturn = await _repo.ShowProduct(id);
            if(productToreturn!=null)
            {
                return Ok(productToreturn);
            }

            return NotFound("The product you are looking does not exist");
        }


        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]ProductDto product)
        {
            Product p = new Product();
            p.ProductName= product.ProductName;
            p.Description = product.Description;
            p.Price = product.Price;
            var createdProduct =  await _repo.SaveProduct(p);
            //return CreatedAtRoute("GetProduct",new {id = createdProduct.ProductId}, createdProduct);
            return createdProduct;
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody]Product product)
        {
            var result = await _repo.ModifyProduct(id, product);
            return Ok(result);
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            bool result;

            try{
                
                result =  await _repo.RemoveProduct(id);
                return Ok(result);
            }

            catch(ArgumentNullException)
            {
                return NotFound("The Product you are trying to delete does not exist");
            }

        }
    }
}