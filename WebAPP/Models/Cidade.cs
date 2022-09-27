using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPP.Models
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
        [Display(Name = "Cidade")]
        public string Nome { get; set; } = null!;
        [Column("estadoId")]
        public int EstadoId { get; set; }

        [ForeignKey("EstadoId")]
        [InverseProperty("Cidades")]
        public virtual Estado Estado { get; set; } = null!;
        [InverseProperty("Cidade")]
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
