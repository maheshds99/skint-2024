using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skinet.Data;
using skinet.Models.Entities;

namespace skinet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await dbContext.Products.ToListAsync();
        }

        [HttpGet("{id:guid}")]   //api/products/2

        public async Task<ActionResult<Product>> GetProduct(Guid Id)
        {
            var product = await dbContext.Products.FindAsync(Id);
            if(product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateProduct(Guid Id, Product product)
        {
            if (product.Id != Id || !ProductExists(Id))
                return BadRequest("Can not update this product");
            dbContext.Entry(product).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteProduct(Guid Id)
        {
            var product = await dbContext.Products.FindAsync(Id);
            if (product == null) return NotFound();
             dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(Guid Id)
        {
            return dbContext.Products.Any(x => x.Id == Id);
        }

    }

}