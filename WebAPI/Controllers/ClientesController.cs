using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly WebAPIContext _context;

        public ClientesController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetCliente(string? cpf, string? nome, DateTime? dataNascimento, string? sexo, string? estado, string? cidade)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }

            var query = _context.Cliente.AsQueryable();

            if (cpf != null)
            {
                query = query.Where(c => c.Cpf.Equals(cpf));
            }

            if (nome != null)
            {
                query = query.Where(c => c.Nome.Contains(nome));
            }

            if (dataNascimento != null)
            {
                query = query.Where(c => c.DataNascimento.Equals(dataNascimento));
            }

            if (sexo != null)
            {
                query = query.Where(c => c.Sexo.Equals(sexo));
            }

            if (estado != null)
            {
                query = query.Where(c => c.Cidade.Estado.Nome.Contains(estado));
            }

            if (cidade != null)
            {
                query = query.Where(c => c.Cidade.Nome.Contains(cidade));
            }

            return await query
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .Select(x => ClienteParaDTO(x))
                .ToListAsync();
        }

        // GET: api/Clientes/12345678900
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(string id)
        {
            var cliente = await _context.Cliente
                .Where(c => c.Cpf == id)
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .Select(x => ClienteParaDTO(x))
                .FirstOrDefaultAsync();

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, ClienteDTO cliente)
        {
            if (id != cliente.Cpf)
            {
                return BadRequest();
            }

            Cliente clienteContext = await _context.Cliente
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .FirstOrDefaultAsync(ci => ci.Cpf == id);
            if (clienteContext == null)
            {
                return NotFound();
            }

            clienteContext.Cpf = cliente.Cpf;
            clienteContext.Nome = cliente.Nome;
            clienteContext.DataNascimento = cliente.DataNascimento;
            clienteContext.Sexo = cliente.Sexo;
            clienteContext.Endereco = cliente.Endereco;
            clienteContext.CidadeId = cliente.CidadeId;
            clienteContext.Cidade = _context.Cidade.Where(c => c.CidadeId == cliente.CidadeId).FirstOrDefault();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDTO clienteDTO)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'WebAPIContext.Cliente'  is null.");
            }

            Cliente cliente = new Cliente()
            {
                Cpf = clienteDTO.Cpf,
                Nome = clienteDTO.Nome,
                DataNascimento = clienteDTO.DataNascimento,
                Sexo = clienteDTO.Sexo,
                Endereco = clienteDTO.Endereco,
                CidadeId = clienteDTO.CidadeId,
                Cidade = _context.Cidade.Where(c => c.CidadeId == clienteDTO.CidadeId).FirstOrDefault()
        };

            _context.Cliente.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.Cpf))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.Cpf }, cliente);
        }

        // DELETE: api/Clientes/12345678900
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return _context.Cliente.Any(e => e.Cpf == id);
        }

        static ClienteDTO ClienteParaDTO(Cliente cliente) =>
            new ClienteDTO
            {
                Cpf = cliente.Cpf,
                Nome = cliente.Nome,
                DataNascimento = cliente.DataNascimento,
                Sexo = cliente.Sexo,
                Endereco = cliente.Endereco,
                EstadoId = cliente.Cidade.EstadoId,
                EstadoNome = cliente.Cidade.Estado.Nome,
                CidadeId = cliente.CidadeId,
                CidadeNome = cliente.Cidade.Nome
            };
    }
}
