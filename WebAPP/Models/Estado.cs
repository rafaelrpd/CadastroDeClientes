using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPP.Models
{
    [Table("estados")]
    public partial class Estado
    {
        public Estado()
        {
            Cidades = new HashSet<Cidade>();
        }

        [Key]
        [Column("estadoId")]
        public int EstadoId { get; set; }
        [Column("nome")]
        [Unicode(false)]
        public string Nome { get; set; } = null!;
        [Column("sigla")]
        [StringLength(2)]
        [Unicode(false)]
        [Display(Name = "Estado")]
        public string Sigla { get; set; } = null!;

        [InverseProperty("Estado")]
        public virtual ICollection<Cidade> Cidades { get; set; }
    }
}
