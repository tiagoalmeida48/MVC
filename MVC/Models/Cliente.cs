using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace MVC.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Conta = new HashSet<Contum>();
        }

        public int CodCli { get; set; }
        public int CodTipoCli { get; set; }

        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Data de nascimento do cliente é obrigatório")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy})", ApplyFormatInEditMode = true)]
        public DateTime DataNascFund { get; set; }

        [Required(ErrorMessage = "Renda ou lucro do cliente é obrigatório")]
        public decimal RendaLucro { get; set; }
        public string Sexo { get; set; }

        [Required(ErrorMessage = "E-mail do cliente é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Endereço do cliente é obrigatório")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "CPF ou CNPJ do cliente é obrigatório")]
        public string Documento { get; set; }
        public string TipoEmpresa { get; set; }

        public virtual TipoCli CodTipoCliNavigation { get; set; }
        public virtual ICollection<Contum> Conta { get; set; }
    }
}
