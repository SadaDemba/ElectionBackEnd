using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.AspNetCore.Mvc;

namespace ElectionBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectorController: ControllerBase
    {
        private readonly IElector @es;
        public ElectorController(IElector es)
        {
            this.es = es;
        }

        // GET: api/Elector
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elector>>> GetElectors()
        {
            var electors = await es.GetElecters();
            return Ok(electors);
        }

        // Get: api/Elector/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Elector>> GetElector(int id)
        {
            Elector electer = await es.GetElecter(id);
            return Ok(electer);
        }

        // Get: api/Elector/CountElectors
        [HttpGet("CountElectors")]
        public async Task<ActionResult<int>> CountElector()
        {
            return await es.CountElectors();
        }

        // Get: api/Elector/CountVotedElectors
        [HttpGet("CountVotedElectors")]
        public async Task<ActionResult<int>> CountVotedElector()
        {
            return await es.CountVotedElectors();
        }

        // Get: api/Elector/CountVotedElectors/Candidate/1
        [HttpGet("CountVotedElectors/Candidate/{id}")]
        public async Task<ActionResult<int>> CountVotedElectorForACand(int id)
        {
            return await es.CountVotedElectorsForACand(id);
        }

        // Get: api/Elector/CountElectors/ByDesk/1
        [HttpGet("CountElectors/ByDesk/{id}")]
        public async Task<ActionResult<int>> CountElectorByDesk(int id)
        {
            return await es.CountElectorsByDesk(id);
        }

        // Get: api/Elector/CountVotedElectors/ByDesk/1
        [HttpGet("CountVotedElectors/ByDesk/{id}")]
        public async Task<ActionResult<int>> CountVotedElectorByDesk(int id)
        {
            return await es.CountVotedElectorsByDesk(id);
        }

        // Get: api/Elector/CountElectors/ByDesk/1/Candidate/2
        [HttpGet("CountElectors/ByDesk/{idD}/Candidate/{idC}")]
        public async Task<ActionResult<int>> CountElectorByDeskForACand(int idD, int idC)
        {
            return await es.CountVotedElectorsByDeskForACand(idD, idC);
        }

        // Get: api/Elector/CountElectors/ByCenter/1
        [HttpGet("CountElectors/ByCenter/{id}")]
        public async Task<ActionResult<int>> CountElectorByCenter(int id)
        {
            return await es.CountElectorsByCenter(id);
        }

        // Get: api/Elector/CountVotedElectors/ByCenter/1
        [HttpGet("CountVotedElectors/ByCenter/{id}")]
        public async Task<ActionResult<int>> CountVotedElectorByCenter(int id)
        {
            return await es.CountVotedElectorsByCenter(id);
        }

        // Get: api/Elector/CountVotedElectors/ByCenter/1}/Candidate/34
        [HttpGet("CountVotedElectors/ByCenter/{idCenter}/Candidate/{idCandidate}")]
        public async Task<ActionResult<int>> CountVotedElectorByCenterForACand(int idCenter, int idCandidate)
        {
            return await es.CountVotedElectorsByCenterForACand(idCenter, idCandidate);
        }

        // Get: api/Elector/CountElectors/ByRegion/1
        [HttpGet("CountElectors/ByRegion/{region}")]
        public async Task<ActionResult<int>> CountElectorByRegion(string region)
        {
            return await es.CountElectorsByRegion(region);
        }

        // Get: api/Elector/CountVotedElectors/ByRegion/1
        [HttpGet("CountVotedElectors/ByRegion/{region}")]
        public async Task<ActionResult<int>> CountVotedElectorByRegion(string region)
        {
            return await es.CountVotedElectorsByRegion(region);
        }

        // Get: api/Elector/CountVotedElectors/ByRegion/dakarCandidate/234
        [HttpGet("CountVotedElectors/ByRegion/{region}/Candidate/{id}")]
        public async Task<ActionResult<int>> CountVotedElectorByRegion(string region, int id)
        {
            return await es.CountVotedElectorsByRegionForACand(region, id);
        }


        // Get: api/Elector/SameDesk/1
        [HttpGet("SameDesk/{id}")]
        public async Task<ActionResult<IEnumerable<Elector>>> GetElecterWithSameDesk(int deskId)
        {
            var electers = await es.GetElectersWithSameDesk(deskId);
            return Ok(electers);
        }

        //Post: api/Elector
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Elector electer)
        {
            await es.PostElecter(electer);
            return Ok();
        }

        //Put: api/Elector
        [HttpPut("{id}")]
        public async Task<ActionResult<Elector>> Put(int id, [FromBody] Elector electer)
        {
            var e = await es.UpdateElecter(id, electer);
            if (e is null)
                return NotFound();
            return Ok(e);
        }

        //Put: api/Elector/1/VoteFor/3
        [HttpPut("{id}/VoteFor/{idC}")]
        public async Task<ActionResult<Elector>> Vote(int id, int idC)
        {
            await es.Vote(id, idC);
            
            return Ok();
        }

        // Delete: api/Elector/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await es.DeleteElecter(id);
            return Ok();
        }





    }
}
