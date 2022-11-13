using Microsoft.EntityFrameworkCore;
using ElectionBackEnd.Model;
using ElectionBackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElectionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidate @ic;

        public CandidatesController(ICandidate icandidate)
        {
            @ic = icandidate;
        }

        // GET: api/Candidates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            var c = await ic.GetCandidates();
            return Ok(c);
        }

        // GET: api/Candidates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var c = await ic.GetCandidate(id);
            return Ok(c);
        }

        // PUT: api/Candidates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, Candidate candidate)
        {
            var ad = await ic.UpdateCandidate(id, candidate);
            if (ad is null)
                return NotFound();
            return Ok(ad);
        }

        // POST: api/Candidates
        [HttpPost]
        public async Task<ActionResult<Candidate>> PostCandidate(Candidate candidate)
        {
            await ic.PostCandidate(candidate);
            return Ok();
        }

        // DELETE: api/Candidates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCandidate(int id)
        {
            await ic.DeleteElecter(id);
            return Ok();
        }
    }
}
