using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteMercado.Desafio.Entities.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Informe {0}")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe {0}")]
        [Display(Name = "Valor de Venda")]
        public decimal ValorVenda { get; set; }

        [Display(Name = "Imagem")]
        public string Imagem { get; set; }
    }
}
