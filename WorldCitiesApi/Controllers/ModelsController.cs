using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCitiesApi.Dtos;
using WorldModel;

namespace WorldCitiesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly WorldCitiesContext _context;

        public ModelsController(WorldCitiesContext context)
        {
            _context = context;
        }

        // GET: api/Models
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Model>>> GetModels()
        {
            return await _context.Models.ToListAsync();
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Model>> GetModel(int id)
        {
            Model? model = await _context.Models.FindAsync(id);
            return model == null ? NotFound() : model;
        }

        // PUT: api/Models/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(int id, Model model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            Model? modelS = await _context.Models.FindAsync(id);

            modelS.Name = model.Name;
            modelS.Description = model.Description;
            modelS.Sales = model.Sales;

            _context.Entry(modelS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Models
        [HttpPost]
        public async Task<ActionResult<Model>> PostCountry(Model model)
        {
            _context.Models.Add(model);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetModel", new { id = model.Id }, model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(int id)
        {
            Model? model = await _context.Models.FindAsync(id);
            ModelDto? modelDto = await _context.Models.Where(c => c.Id == id)
            .Select(c => new ModelDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Sales = c.Sales,
                Cars = c.Cars,
                CountingNum = c.Cars.Count(),
            }).SingleOrDefaultAsync();
            if (model == null)
            {
                return Ok(new ApiResponse<ModelDto>
                {
                    Status = 604,
                    Message = "This model cannot be found!",
                    Data = null
                });
            }
            else
            {

                if (modelDto.CountingNum != 0)
                {
                    return Ok(new ApiResponse<ModelDto>
                    {
                        Status = 603,
                        Message = "There are some sub-data binding this model. We can't delete it now!",
                        Data = null
                    });
                }
                else
                {
                    _context.Models.Remove(model);
                    await _context.SaveChangesAsync();
                    return Ok(new ApiResponse<ModelDto>
                    {
                        Status = 200,
                        Message = "This model has deleted!",
                        Data = modelDto
                    });
                }
            }
        }

        [HttpGet("GetModelSub/{id}")]
        public async Task<ActionResult<ModelDto>> GetModelSub(int id)
        {
            ModelDto? modelDto = await _context.Models.Where(c => c.Id == id)
                .Select(c => new ModelDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Sales = c.Sales,
                    Cars = c.Cars,
                    CountingNum = c.Cars.Count(),
                }).SingleOrDefaultAsync();
            return modelDto is null ? NotFound() : modelDto;
        }
        private bool ModelExists(int id) => _context.Models.Any(e => e.Id == id);
    }
}
