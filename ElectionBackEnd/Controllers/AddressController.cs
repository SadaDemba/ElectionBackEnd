using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddress @ia;
        public AddressController(IAddress ia)
        {
            this.ia = ia;
        }

        // GET: api/<AddressController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var addresses = await ia.GetAddresses();
            return Ok(addresses);
        }

        // GET: api/<AddressController>
        [HttpGet("Region/{region}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddressesByRegion(string region)
        {
            var addresses = await ia.GetAddressesByRegion(region);
            return Ok(addresses);
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await ia.GetAddress(id);
            return Ok(address);
        }

        [HttpGet("Distance/{id1}/{id2}")]
        public ActionResult<Address> GetDistance(int id1, int id2)
        {
            var address = ia.GetDistance(id1, id2);
            return Ok(address);
        }

        // POST api/<AddressController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Address address)
        {
            await ia.Post(address);
            return Ok();
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Address>> Put(int id, [FromBody] Address address)
        {
            var ad =await ia.Put(id, address);
            if (ad is null)
                return NotFound();
            return Ok(ad);
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await ia.Delete(id);
            return Ok();
        }
    }
}
