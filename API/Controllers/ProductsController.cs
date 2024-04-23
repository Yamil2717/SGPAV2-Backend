using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly InventoryContext _ProductContext;

        public ProductsController(InventoryContext context)
        {
            _ProductContext = context;
        }

        [HttpGet]
        public IEnumerable<Products> Get()
        {
            return _ProductContext.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Products> Get(int id)
        {
            var product = _ProductContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public ActionResult<Products> Post(Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _ProductContext.Products.Add(product);
            _ProductContext.SaveChanges();

            return CreatedAtAction("Get", new { id = product.ID }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Products product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            _ProductContext.Entry(product).State = EntityState.Modified;
            _ProductContext.SaveChanges();

            return NoContent();
        }
    }
}
