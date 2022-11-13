using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElectionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationController : ControllerBase
    {
        private readonly IAllocation @il;
        public AllocationController(IAllocation allocation)
        {
            @il = allocation;
        }

        // GET api/<AllocationController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Desk>> AssignBestCenter(int id)
        {
            var desk = await il.AssignBestCenter(id);
            return Ok(desk);
        }
    }
}
