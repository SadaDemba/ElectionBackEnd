using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ICenter @ic;
        public CenterController(ICenter ic)
        {
            this.ic = ic;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Center>>> Get()
        {
            var centers = await ic.GetCenters();
            return Ok(centers);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Center>> Get(int id)
        {
            var center = await ic.Get(id);
            return Ok(center);
        }

        [HttpGet("SameAddress/{id}")]
        public async Task<ActionResult<IEnumerable<Center>>> GetSameAddress(int id)
        {
            var centers = await ic.GetDesksWithSameAddress(id);
            return Ok(centers);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Center center)
        {
            var c = await ic.Post(center);
            if (c is null) return BadRequest("Nombre de bureaux -{0}- Insensé " + center.Nb_Desk);
            return Ok(c);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Center>> Put(int id, [FromBody] Center center)
        {
            var c = await ic.Put(id, center);
            if (c is null)
                return NotFound();
            return Ok(c);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await ic.Delete(id);
            return Ok();
        }
    }
}
