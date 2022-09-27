using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public CidadesController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Cidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadeDTO>>> GetCidade(int? cidadeId, string? cidadeNome, int? estadoId, string? estadoNome)
        {
            if (_context.Cidade == null)
            {
                return NotFound();
            }

            var query = _context.Cidade.AsQueryable();

            if (cidadeId != null)
            {
                query = query.Where(c => c.CidadeId.Equals(cidadeId));
            }

            if (cidadeNome != null)
            {
                query = query.Where(c => c.Nome.Contains(cidadeNome));
            }

            if (estadoId != null)
            {
                query = query.Where(c => c.Estado.EstadoId.Equals(estadoId));
            }

            if (estadoNome != null)
            {
                query = query.Where(c => c.Estado.Nome.Contains(estadoNome));
            }

            return await query
                .Include(c => c.Estado)
                .Select(c => CidadeParaDTO(c))
                .ToListAsync();
        }

        // GET: api/Cidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CidadeDTO>> GetCidade(int id)
        {
            var cidade = await _context.Cidade
                .Where(c => c.CidadeId == id)
                .Include(e => e.Estado)
                .Select(x => CidadeParaDTO(x))
                .FirstOrDefaultAsync();

            if (cidade == null)
            {
                return NotFound();
            }

            return cidade;
        }

        // PUT: api/Cidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCidade(int id, CidadeDTO cidade)
        {
            if (id != cidade.CidadeId)
            {
                return BadRequest();
            }

            Cidade cidadeContext = await _context.Cidade
                .Include(e => e.Estado)
                .FirstOrDefaultAsync();

            if (cidadeContext == null)
            {
                return NotFound();
            }

            cidadeContext.Nome = cidade.CidadeNome;
            cidadeContext.EstadoId = cidade.EstadoId;
            cidadeContext.Estado = _context.Estado.Where(ei => ei.EstadoId == cidade.EstadoId).FirstOrDefault();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CidadeExists(id))
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

        // POST: api/Cidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cidade>> PostCidade(CidadePostDTO cidadePostDTO)
        {
            if (_context.Cidade == null)
            {
                return Problem("Entity set 'WebAPIContext.Cidade'  is null.");
            }

            var estado = _context.Estado.Where(e => e.EstadoId == cidadePostDTO.EstadoId).FirstOrDefault();

            Cidade cidade = new Cidade()
            {
                Nome = cidadePostDTO.CidadeNome,
                EstadoId = cidadePostDTO.EstadoId,
                Estado = estado
            };

            _context.Cidade.Add(cidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCidade", new { id = cidade.CidadeId }, cidadePostDTO);
        }

        // DELETE: api/Cidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade(int id)
        {
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }

            _context.Cidade.Remove(cidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CidadeExists(int id)
        {
            return _context.Cidade.Any(e => e.CidadeId == id);
        }

        static CidadeDTO CidadeParaDTO(Cidade cidade) =>
            new CidadeDTO
            {
                CidadeId = cidade.CidadeId,
                CidadeNome = cidade.Nome,
                EstadoId = cidade.Estado.EstadoId,
                EstadoNome = cidade.Estado.Nome
            };
    }
}
