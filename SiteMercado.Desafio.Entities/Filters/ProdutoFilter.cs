namespace SiteMercado.Desafio.Entities.Filters
{
    public class ProdutoFilter : PaginationFilter
    {
        public long? Id { get; set; }

        public string Nome { get; set; }

        public decimal? ValorVenda { get; set; }

        public string Imagem { get; set; }

        public string search { get; set; }
    }
}
