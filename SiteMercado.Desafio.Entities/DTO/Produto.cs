using System;

namespace SiteMercado.Desafio.Entities.DTO
{
    public class Produto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorVenda { get; set; }
        public string Imagem { get; set; }
    }
}
