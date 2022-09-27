using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public EstadosController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Estadoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> GetEstado(int? estadoId, string? estadoNome, string? sigla)
        {
            if (_context.Estado == null)
            {
                return NotFound();
            }

            var query = _context.Estado.AsQueryable();

            if (estadoId != null)
            {
                query = query.Where(x => x.EstadoId.Equals(estadoId));
            }

            if (estadoNome != null)
            {
                query = query.Where(x => x.Nome.Contains(estadoNome));
            }

            if (sigla != null)
            {
                query = query.Where(x => x.Sigla.Contains(sigla));
            }



            return await query.Select(e => EstadoParaDTO(e))
                .ToListAsync();
        }

        // Redundante?
        // GET: api/Estadoes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Estado>> GetEstado(int id)
        //{
        //    var estado = await _context.Estado
        //        .Where(e => e.EstadoId == id)
        //        .FirstOrDefaultAsync();

        //    estado.Cidades = await _context.Cidade.Where(c => c.EstadoId == id).ToListAsync();

        //    if (estado == null)
        //    {
        //        return NotFound();
        //    }

        //    return estado;
        //}

        // PUT: api/Estadoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstado(int id, EstadoDTO estado)
        {
            if (id != estado.EstadoId)
            {
                return BadRequest();
            }

            Estado estadoContext = await _context.Estado.FindAsync(id);

            if (estadoContext == null)
            {
                return NotFound();
            }

            estadoContext.Nome = estado.Nome;
            estadoContext.Sigla = estado.Sigla;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(id))
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

        // POST: api/Estadoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(EstadoPostDTO estadoPostDTO)
        {
            if (_context.Estado == null)
            {
                return Problem("Entity set 'WebAPIContext.Estado'  is null.");
            }

            Estado estado = new Estado()
            {
                Nome = estadoPostDTO.Nome,
                Sigla = estadoPostDTO.Sigla,
                Cidades = null
            };

            _context.Estado.Add(estado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstado", new { id = estado.EstadoId }, estadoPostDTO);
        }

        // DELETE: api/Estadoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstado(int id)
        {
            var estado = await _context.Estado.FindAsync(id);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(int id)
        {
            return _context.Estado.Any(e => e.EstadoId == id);
        }

        static EstadoDTO EstadoParaDTO(Estado estado) =>
            new EstadoDTO
            {
                EstadoId = estado.EstadoId,
                Nome = estado.Nome,
                Sigla = estado.Sigla
            };
    }
}
