using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    [Table("cidades")]
    public partial class Cidade
    {
        public Cidade()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        [Column("cidadeId")]
        public int CidadeId { get; set; }
        [Column("nome")]
        [Unicode(false)]
        public string Nome { get; set; } = null!;
        [Column("estadoId")]
        public int EstadoId { get; set; }

        [ForeignKey("EstadoId")]
        [InverseProperty("Cidades")]
        public virtual Estado Estado { get; set; } = null!;
        [InverseProperty("Cidade")]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }

    public class CidadeDTO
    {
        public int CidadeId { get; set; }
        public string CidadeNome { get; set; } = null!;
        public int EstadoId { get; set; }
        public string EstadoNome { get; set; } = null!;
    }

    public class CidadePostDTO
    {
        public string CidadeNome { get; set; } = null!;
        public int EstadoId { get; set; }
        public string EstadoNome { get; set; } = null!;
    }
}
