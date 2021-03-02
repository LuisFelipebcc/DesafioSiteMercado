using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SiteMercado.Desafio.Business;
using SiteMercado.Desafio.Entities.Filters;
using SiteMercado.Desafio.Entities.Models;
using SiteMercado.Desafio.Utils.Models;

namespace SiteMercado.Desafio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : APIController
    {
        public ProdutoController(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Endpoint para obter lista de Produtos
        /// </summary>
        /// <param name="filter">Filtro para buscar determinadas lista de Produtos</param>
        /// <returns>Lista de Produtos</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("getList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> GetList([FromQuery]ProdutoFilter filter)
        {
            using (var business = new ProdutoBusiness())
            {
                filter.ProcessQueryString(Request.Query);
                var data = business.GetList(filter);
                return Ok(data);
            }
        }

        /// <summary>
        /// EndPoint para inserir um novo Produto
        /// </summary>
        /// <param name="usuario">Objeto para inserir o Produto</param>
        /// <returns>Confirmação/Resultado da inserção do Produto</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Insert")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> Insert(Produto produto)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Insert(produto);
                return Ok(data);
            }
        }

        /// <summary>
        /// EndPoint para alterer um Produto
        /// </summary>
        /// <param name="produto">Objeto para alterer o Produto</param>
        /// <returns>Confirmação/Resultado da altereção do Produto</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Update")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> Update(Produto produto)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Update(produto);
                return Ok(data);
            }
        }

        /// <summary>
        /// EndPoint para excluir um produto, caso o produto esteja vincolado a algo, não será feito a exclusão, mas sim será desativado
        /// </summary>
        /// <param name="id">Id de Produto</param>
        /// <returns>Confirmação/Resultado da Exclusão do Produto</returns>
        [HttpDelete("Delete/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult<ResultModel<Produto>>), 200)]
        public ActionResult<ResultModel<Produto>> Delete(long id)
        {
            using (var business = new ProdutoBusiness())
            {
                var data = business.Delete(new Produto() { Id = id });
                return Ok(data);
            }
        }

    }
}
