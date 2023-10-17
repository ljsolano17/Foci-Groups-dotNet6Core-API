using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Solution.DAL.EF;
using data = Solution.DO.Objects;

namespace Solution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly SolutionDbContext _context;

        public GroupsController(SolutionDbContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<data.Group>>> GetGroups()
        {
            return new  Solution.BS.Group(_context).GetAll().ToList();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<data.Group>> GetGroup(int id)
        {
            
            var group =  new Solution.BS.Group(_context).GetOneById(id);


            if (group == null)
            {
                return NotFound();
            }

            return group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, data.Group group)
        {
            if (id != group.GroupId)
            {
                return BadRequest();
            }

            try
            {
                new Solution.BS.Group(_context).Update(group);
            }
            catch (Exception ee)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<data.Group>> PostGroup(data.Group group)
        {
           
            new Solution.BS.Group(_context).Insert(group);

            return CreatedAtAction("GetGroup", new { id = group.GroupId }, group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<data.Group>> DeleteGroup(int id)
        {
            
            var group = new Solution.BS.Group(_context).GetOneById(id);
            if (group == null)
            {
                return NotFound();
            }

            new Solution.BS.Group(_context).Delete(group);

            return group;
        }

        private bool GroupExists(int id)
        {
            //Validar si existe el grupo
            return (new Solution.BS.Group(_context).GetOneById(id)!=null);
        }
    }
}
