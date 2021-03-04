using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SiteMercado.Desafio.Entities.DTO
{
    public class Produto
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("valorVenda")]
        public decimal ValorVenda { get; set; }

        [JsonProperty("imagem")]
        public string Imagem { get; set; }
    }
}
