using Microsoft.AspNetCore.Mvc;
using DB;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesInterface _salesRepository;

        public SalesController(SalesInterface salesRepository)
        {
            _salesRepository = salesRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Sale>> Get()
        {
            return Ok(_salesRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Sale> Get(int id)
        {
            var sale = _salesRepository.Get(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        [HttpPost]
        public ActionResult<Sale> Post(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdSale = _salesRepository.Add(sale);

            return CreatedAtAction(nameof(Get), new { id = createdSale.ID }, createdSale);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Sale sale)
        {
            if (id != sale.ID)
            {
                return BadRequest();
            }

            var updatedSale = _salesRepository.Update(id, sale);

            if (updatedSale == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sale = _salesRepository.Get(id);

            if (sale == null)
            {
                return NotFound();
            }

            _salesRepository.Delete(id);

            return NoContent();
        }
    }
}