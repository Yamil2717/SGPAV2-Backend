using Microsoft.AspNetCore.Mvc;
using DB;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryInterface _categoryRepository;

        public CategoryController(CategoryInterface categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categories>> Get()
        {
            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Categories> Get(int id)
        {
            var category = _categoryRepository.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public ActionResult<Categories> Post(Categories category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCategory = _categoryRepository.Add(category);

            return CreatedAtAction(nameof(Get), new { id = createdCategory.ID }, createdCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Categories category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            var updatedCategory = _categoryRepository.Update(id, category);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.Delete(id);

            return NoContent();
        }
    }
}