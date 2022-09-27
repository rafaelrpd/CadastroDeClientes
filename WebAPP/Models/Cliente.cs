using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebAPP.Models
{
    [Table("clientes")]
    public partial class Cliente
    {
        [Key]
        [Column("cpf")]
        [StringLength(11)]
        [Unicode(false)]
        [Display(Name = "CPF")]
        [DisplayFormat(DataFormatString = "{0:###.###.###-##}")]
        public string Cpf { get; set; } = null!;
        [Column("nome")]
        [Unicode(false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;
        [Column("dataNascimento", TypeName = "date")]
        [Display(Name = "Data Nasc.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [Column("sexo")]
        [StringLength(1)]
        [Unicode(false)]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; } = null!;
        [Column("endereco")]
        [Unicode(false)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; } = null!;
        [Column("cidadeId")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }

        [ForeignKey("CidadeId")]
        [InverseProperty("Clientes")]
        public virtual Cidade Cidade { get; set; } = null!;
    }
}
