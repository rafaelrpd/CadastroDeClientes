using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
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
        public string Sigla { get; set; } = null!;

        [InverseProperty("Estado")]
        public virtual ICollection<Cidade> Cidades { get; set; }
    }

    public class EstadoDTO
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; } = null!;
        public string Sigla { get; set; } = null!;
    }

    public class EstadoPostDTO
    {
        public string Nome { get; set; } = null!;
        public string Sigla { get; set; } = null!;
    }

    //public class EstadoDetalhesDTO
    //{
    //    public int EstadoId { get; set; }
    //    public string Nome { get; set; } = null!;
    //    public string Sigla { get; set; } = null!;
    //    public List<CidadeDTO> Cidades { get; set; }
    //}
}
