using Microsoft.AspNetCore.Mvc.Rendering;
using WebAPP.Models;

namespace WebAPP.ViewModel
{
    public class ClientesListarViewModel
    {
        public IEnumerable<Cliente> Clientes { get; set; }
        public SelectList Cidades { get; set; }
        public SelectList Estados { get; set; }
    }
}
