using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCitiesApi.Dtos;
using WorldModel;

namespace WorldCitiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly WorldCitiesContext _context;

        public MakesController(WorldCitiesContext context)
        {
            _context = context;
        }

        // GET: api/Makes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            return await _context.Makes.ToListAsync();
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Make>> GetMake(int id)
        {
            Make? make = await _context.Makes.FindAsync(id);
            return make == null ? NotFound() : make;
        }

        // PUT: api/Makes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id, Make make)
        {
            if (id != make.Id)
            {
                return BadRequest();
            }

            _context.Entry(make).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Makes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Make>> PostMake(Make make)
        {
            Make makesql = new()
            {
                Name = make.Name,
                Country = make.Country,
                Description = make.Description,
            };

            make.Id = 0;
            _context.Makes.Add(makesql);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetMake", new { id = make.Id }, make);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            Make? make = await _context.Makes.FindAsync(id);
            MakeDto? makeDto = await _context.Makes.Where(c => c.Id == id)
                .Select(c => new MakeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country,
                    Description = c.Description,
                    Models = c.Models,
                    CountingNum = c.Models.Count(),
                }).SingleOrDefaultAsync();
            if (make == null)
            {
                return Ok(new ApiResponse<MakeDto>
                {
                    Status = 604,
                    Message = "This make cannot be found!",
                    Data = null
                });
            }
            else
            {

                if (makeDto.CountingNum != 0)
                {
                    return Ok(new ApiResponse<ModelDto>
                    {
                        Status = 603,
                        Message = "There is data binding this make, this make cannot be deleted now!",
                        Data = null
                    });
                }
                else
                {
                    _context.Makes.Remove(make);
                    await _context.SaveChangesAsync();
                    return Ok(new ApiResponse<MakeDto>
                    {
                        Status = 200,
                        Message = "This make has deleted!",
                        Data = makeDto
                    });
                }
            }
        }

        [HttpGet("GetMakeSub/{id}")]
        public async Task<ActionResult<MakeDto>> GetMakeSub(int id)
        {
            MakeDto? makeDto = await _context.Makes.Where(c => c.Id == id)
                .Select(c => new MakeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country,
                    Description = c.Description,
                    Models = c.Models,
                    CountingNum = c.Models.Count(),
                }).SingleOrDefaultAsync();
            return makeDto is null ? NotFound() : makeDto;
        }

        // GET: api/Makesdetails/5
        [HttpGet("Makesdetails/{id}")]
        public async Task<ActionResult<MakeDto>> GetMakeDetails(int id)
        {
            MakeDto? makeDto = await _context.Makes.Where(c => c.Id == id)
                .Select(c => new MakeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Country = c.Country,
                    Description = c.Description,
                    Models = c.Models,
                    CountingNum = c.Models.Count(),
                    Sumsales = c.Models.Sum(t => t.Sales),
                }).SingleOrDefaultAsync();
            return makeDto is null ? NotFound() : makeDto;
        }

        private bool MakeExists(int id) => _context.Makes.Any(e => e.Id == id);
/*        public int CountCar(int id) => */
    }
}
