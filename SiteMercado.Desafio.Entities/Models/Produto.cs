using System.ComponentModel.DataAnnotations;

namespace SiteMercado.Desafio.Entities.Models
{
    public class Produto
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Informe {0}")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe {0}")]
        [Display(Name = "Valor de Venda")]
        public decimal ValorVenda { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }
    }
}
