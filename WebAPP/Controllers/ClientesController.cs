using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAPP.Data;
using WebAPP.Models;
using WebAPP.ViewModel;

namespace WebAPP.Controllers
{
    public class ClientesController : Controller
    {
        private readonly WebAPPContext _context;

        public ClientesController(WebAPPContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = _context.Cliente
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .ToList();

            string cpf = string.Empty;
            string nome = string.Empty;
            DateTime? dataNascimento = null;
            string sexo = string.Empty;
            string estado = string.Empty;
            string cidade = string.Empty;


            var clientesListarViewModel = new ClientesListarViewModel
            {
                Clientes = clientes,
                Cidades = new SelectList(_context.Set<Cidade>(), "CidadeId", "Nome"),
                Estados = new SelectList(_context.Set<Estado>(), "EstadoId", "Sigla")
            };

            return View(clientesListarViewModel);
        }

        // Não estou usando
        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Cidade)
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["Cidade"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "Nome");
            ViewData["Estado"] = new SelectList(_context.Set<Estado>(), "EstadoId", "Sigla");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cpf,Nome,DataNascimento,Sexo,Endereco,CidadeId")] Cliente cliente)
        {
            var cidade = await _context.Cidade
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(c => c.CidadeId == cliente.CidadeId);
            cliente.Cidade = cidade;
            ModelState.Clear();
            TryValidateModel(cliente);

            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cidade"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "Nome", cliente.CidadeId);
            ViewData["Estado"] = new SelectList(_context.Set<Estado>(), "EstadoId", "Sigla", cliente.Cidade.EstadoId);
            //ViewData["CidadeId"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "CidadeId", cliente.CidadeId);
            //ViewData["EstadoId"] = new SelectList(_context.Set<Estado>(), "EstadoId", "EstadoId", cliente.Cidade.EstadoId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Cidade)
                .FirstOrDefaultAsync(c => c.Cpf == id);

            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["Cidade"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "Nome", cliente.CidadeId);
            ViewData["Estado"] = new SelectList(_context.Set<Estado>(), "EstadoId", "Sigla", cliente.Cidade.EstadoId);

            //ViewData["CidadeId"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "CidadeId", cliente.CidadeId);
            //ViewData["EstadoId"] = new SelectList(_context.Set<Estado>(), "EstadoId", "EstadoId", cliente.Cidade.EstadoId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cpf,Nome,DataNascimento,Sexo,Endereco,CidadeId")] Cliente cliente)
        {
            if (id != cliente.Cpf)
            {
                return NotFound();
            }

            var cidade = await _context.Cidade
                .Include(e => e.Estado)
                .FirstOrDefaultAsync(c => c.CidadeId == cliente.CidadeId);
            cliente.Cidade = cidade;
            ModelState.Clear();
            TryValidateModel(cliente);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Cpf))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cidade"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "Nome", cliente.CidadeId);
            ViewData["Estado"] = new SelectList(_context.Set<Estado>(), "EstadoId", "Sigla", cliente.Cidade.EstadoId);
            //ViewData["CidadeId"] = new SelectList(_context.Set<Cidade>(), "CidadeId", "CidadeId", cliente.CidadeId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.Cidade)
                .Include(e => e.Cidade.Estado)
                .FirstOrDefaultAsync(m => m.Cpf == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Cliente == null)
            {
                return Problem("Entity set 'WebAPPContext.Cliente'  is null.");
            }
            var cliente = await _context.Cliente
                .Include(c => c.Cidade)
                .FirstOrDefaultAsync(c => c.Cpf == id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Buscar(string? cpf, string? nome, DateTime? dataNascimento, string? sexo, string? estado, string? cidade)
        {
            IEnumerable<Cliente> clientes;

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

            clientes = query.ToList();

            return View("~/Views/Clientes/Index.cshtml", new ClientesListarViewModel
            {
                Clientes = clientes
            });
        }

        private bool ClienteExists(string id)
        {
            return _context.Cliente.Any(e => e.Cpf == id);
        }
    }
}
