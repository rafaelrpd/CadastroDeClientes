using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    [Table("clientes")]
    public partial class Cliente
    {
        [Key]
        [Column("cpf")]
        [StringLength(11)]
        [Unicode(false)]
        public string Cpf { get; set; } = null!;
        [Column("nome")]
        [Unicode(false)]
        public string Nome { get; set; } = null!;
        [Column("dataNascimento", TypeName = "date")]
        public DateTime DataNascimento { get; set; }
        [Column("sexo")]
        [StringLength(1)]
        [Unicode(false)]
        public string Sexo { get; set; } = null!;
        [Column("endereco")]
        [Unicode(false)]
        public string Endereco { get; set; } = null!;
        [Column("cidadeId")]
        public int CidadeId { get; set; }

        [ForeignKey("CidadeId")]
        [InverseProperty("Clientes")]
        public virtual Cidade Cidade { get; set; } = null!;
    }

    public class ClienteDTO
    {
        public string Cpf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Endereco { get; set; } = null!;
        public int EstadoId { get; set; }
        public string EstadoNome { get; set; } = null!;
        public int CidadeId { get; set; }
        public string CidadeNome { get; set; } = null!;
    }
}
