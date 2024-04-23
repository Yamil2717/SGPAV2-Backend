using Microsoft.AspNetCore.Mvc;
using DB;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsInterface _productsRepository;

        public ProductsController(ProductsInterface productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Products>> Get()
        {
            return Ok(_productsRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Products> Get(int id)
        {
            var product = _productsRepository.Get(id);

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

            var createdProduct = _productsRepository.Add(product);

            return CreatedAtAction(nameof(Get), new { id = createdProduct.ID }, createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Products product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            var updatedProduct = _productsRepository.Update(id, product);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _productsRepository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productsRepository.Delete(id);

            return NoContent();
        }
    }
}
