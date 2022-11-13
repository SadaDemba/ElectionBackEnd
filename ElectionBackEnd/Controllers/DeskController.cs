using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeskController : ControllerBase
    {
        private readonly IDesk @ifd;
        public DeskController(IDesk id)
        {
            this.ifd = id;
        }

        // GET: api/<DeskController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Desk>>> Get()
        {
            var desks = await ifd.GetDesks();
            return Ok(desks);
        }

        // GET api/<DeskController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desk>> Get(int id)
        {
            var desk = await ifd.GetDesk(id);
            return Ok(desk);
        }

        // GET api/<DeskController>/SameCenter/5
        [HttpGet("SameCenter/{id}")]
        public async Task<ActionResult<IEnumerable<Desk>>> GetSameCenter(int id)
        {
            var desks = await ifd.GetDesksWithSameCenter(id);
            return Ok(desks);
        }

        // POST api/<DeskController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Desk desk)
        {
            var d = await ifd.PostDesk(desk);
            if (d is null) return BadRequest("Capacité de bureau -{0}- Insensé "+ desk.Capacity);
            return Ok(d);
        }

        // PUT api/<DeskController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Desk>> Put(int id, [FromBody] Desk desk)
        {
            var d = await ifd.PutDesk(id, desk);
            if (d is null)
                return NotFound();
            return Ok(d);
        }

        // DELETE api/<DeskController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await ifd.DeleteDesk(id);
            return Ok();
        }
    }
}
