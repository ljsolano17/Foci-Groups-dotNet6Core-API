using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using data = Solution.DO.Objects;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FociController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        public FociController(SolutionDbContext context)
        {
            _context = context;
        }

        // GET: api/Foci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<data.Focus>>> GetFocus()
        {
            return new Solution.BS.Focus(_context).GetAll().ToList();
        }

        // GET: api/Foci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<data.Focus>> GetFocus(int id)
        {
            //var focus = await new Solution.BS.Focus(_context).GetOneByIdWithAsync(id);

            var focus = new Solution.BS.Focus(_context).GetOneById(id);

            if (focus == null)
            {
                return NotFound();
            }

            return focus;
        }

        // PUT: api/Foci/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFocus(int id, data.Focus focus)
        {
            if (id != focus.FocusId)
            {
                return BadRequest();
            }

            try
            {
                new Solution.BS.Focus(_context).Update(focus);
            }
            catch (Exception ee)
            {
                if (!FocusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Foci
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<data.Focus>> PostFoci(data.Focus focus)
        {

            new Solution.BS.Focus(_context).Insert(focus);

            return CreatedAtAction("GetFocus", new { id = focus.FocusId }, focus);
        }

        // DELETE: api/Foci/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<data.Focus>> DeleteFocus(int id)
        {

            var focus = new Solution.BS.Focus(_context).GetOneById(id);
            if (focus == null)
            {
                return NotFound();
            }

            new Solution.BS.Focus(_context).Delete(focus);

            return focus;
        }

        private bool FocusExists(int id)
        {
            return (new Solution.BS.Focus(_context).GetOneById(id) != null);
        }
    }
}
